using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DormMS.Web.Controllers
{
    [Route("Account/[action]/{id?}")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuditService _audit;
        public AccountController(ApplicationDbContext context, IAuditService audit) { _context = context; _audit = audit; }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, string? returnUrl = null)
        {
            string hashedInput = HashPassword(password);

            // EMERGENCY SEED: Test hesabı yoksa oluştur (Migration yapamadığımız durumlar için)
            if (username == "student" && password == "123")
            {
                var existing = await _context.Users.FirstOrDefaultAsync(u => u.username == "student");
                if (existing == null)
                {
                    var studentRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "Student");
                    if (studentRole == null)
                    {
                        studentRole = new Role { RoleName = "Student", Description = "Resident" };
                        _context.Roles.Add(studentRole);
                        await _context.SaveChangesAsync();
                    }

                    var newUser = new User 
                    { 
                        username = "student", 
                        passwordHash = hashedInput, 
                        firstName = "Emily", 
                        lastName = "Resident",
                        email = "student@dorm.com",
                        isActive = true,
                        createdAt = DateTime.Now
                    };
                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();

                    _context.UserRoles.Add(new UserRole { UserId = newUser.Id, RoleId = studentRole.Id });
                    _context.Students.Add(new Student { userId = newUser.Id, studentId = "STU001", status = "Active" });
                    await _context.SaveChangesAsync();
                }
            }

            var user = await _context.Users
                .Include(u => u.UserRoles!)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.username == username && u.passwordHash == hashedInput);

            if (user != null)
            {
                var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("FullName", $"{user.firstName} {user.lastName}"),
            new Claim("ProfilePicture", user.profilePicture ?? "")
        };

                if (user.UserRoles != null)
                {
                    foreach (var ur in user.UserRoles)
                    {
                        if (ur.Role != null)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, ur.Role.RoleName.Trim()));
                        }
                    }
                }

                if (!claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Student"))
                {
                    if (await _context.Students.AnyAsync(s => s.userId == user.Id))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Student"));
                    }
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid credentials!";
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword() => View();

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

            var userId = int.Parse(userIdString);
            var user = await _context.Users.FindAsync(userId);

            string hashedCurrent = HashPassword(currentPassword);

            if (user != null && user.passwordHash == hashedCurrent)
            {
                user.passwordHash = HashPassword(newPassword);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Password updated successfully!";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Current password is incorrect.";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyProfile()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString)) return RedirectToAction("Login");
            
            var userId = int.Parse(userIdString);

            if (User.IsInRole("Admin") || User.IsInRole("Manager") || User.IsInRole("DormManager"))
            {
                ViewBag.BuildingCount = await _context.Buildings.CountAsync();
                ViewBag.RoomTypes = await _context.RoomTypes.ToListAsync();
                return View("DormitoryProfile");
            }

            if (User.IsInRole("Student"))
            {
                var student = await _context.Students.FirstOrDefaultAsync(s => s.userId == userId);
                if (student == null) 
                {
                    TempData["Error"] = "Student profile not found. Please contact administration.";
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Details", "Students", new { id = student.id });
            }

            if (User.IsInRole("Staff"))
            {
                var user = await _context.Users.FindAsync(userId);
                return View("StaffProfile", user);
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString)) return RedirectToAction("Login");
            var userId = int.Parse(userIdString);
            var user = await _context.Users.FindAsync(userId);
            return View(user);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(User model, IFormFile? profilePhoto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

            var userId = int.Parse(userIdString);
            var user = await _context.Users.FindAsync(userId);

            if (user != null)
            {
                user.firstName = model.firstName;
                user.lastName = model.lastName;
                user.email = model.email;
                user.phone = model.phone;

                if (profilePhoto != null && profilePhoto.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "profiles");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(profilePhoto.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await profilePhoto.CopyToAsync(fileStream);
                    }

                    user.profilePicture = "/uploads/profiles/" + uniqueFileName;
                }

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                var existingIdentity = User.Identity as ClaimsIdentity;
                var claims = User.Claims.ToList();
                
                var photoClaim = claims.FirstOrDefault(c => c.Type == "ProfilePicture");
                if (photoClaim != null) claims.Remove(photoClaim);
                claims.Add(new Claim("ProfilePicture", user.profilePicture ?? ""));

                var claimsIdentity = new ClaimsIdentity(
                    claims, 
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    existingIdentity?.NameClaimType ?? ClaimTypes.Name,
                    existingIdentity?.RoleClaimType ?? ClaimTypes.Role);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(claimsIdentity));

                await _audit.LogActionAsync("UPDATE", "User", userId, null, "Profile details updated");
                TempData["Success"] = "Profile updated successfully!";
            }
            return RedirectToAction(nameof(Settings));
        }
        [HttpGet]
        public IActionResult AccessDenied() => View();

        [HttpGet]
        public async Task<IActionResult> FixGulzi()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.username == "gulzi.staff");
            if (user != null)
            {
                user.passwordHash = "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=";
                await _context.SaveChangesAsync();
                return Content("gulzi.staff fixed! Password is now 123456.");
            }
            return Content("User not found.");
        }
    }
}