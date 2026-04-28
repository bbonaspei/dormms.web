using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DormMS.Web.Data;
using DormMS.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DormMS.Web.Interfaces;

namespace DormMS.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Staff/[action]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly ApplicationDbContext _context;

        public StaffController(IStaffService staffService, ApplicationDbContext context)
        {
            _staffService = staffService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var staffList = await _staffService.GetStaffListAsync();
            return View(staffList);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            if (staff == null) return NotFound();

            ViewBag.UserLogs = await _context.AuditLogs
                .Where(l => l.userId == id)
                .OrderByDescending(l => l.createdAt)
                .Take(5)
                .ToListAsync();

            return View(staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(int id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            if (staff == null) return NotFound();

            staff.passwordHash = "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=";
            var success = await _staffService.UpdateStaffAsync(staff);

            if (success)
            {
                TempData["Success"] = $"Access key for {staff.firstName} reset to default (123456).";
            }
            else
            {
                TempData["Error"] = "Failed to reset security credentials.";
            }

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = _context.Roles
                .Where(r => r.RoleName != "Student")
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.RoleName == "Staff" ? "Maintenance" : r.RoleName
                })
                .ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user, int roleId)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users.AnyAsync(u => u.username == user.username);
                if (existingUser)
                {
                    ModelState.AddModelError("username", "Identity Conflict: This credentials (username) is already registered in the system.");
                }
                else
                {
                    var success = await _staffService.AddStaffAsync(user, roleId);
                    if (success) return RedirectToAction(nameof(Index));
                    
                    ModelState.AddModelError("", "Failed to hire staff member. Please check administrative logs.");
                }
            }

            ViewBag.Roles = _context.Roles
                .Where(r => r.RoleName != "Student")
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.RoleName == "Staff" ? "Maintenance" : r.RoleName
                })
                .ToList();
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            if (staff == null) return NotFound();

            var selectedRoleId = staff.UserRoles?.FirstOrDefault()?.RoleId;
            ViewBag.Roles = _context.Roles
                .Where(r => r.RoleName != "Student")
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.RoleName == "Staff" ? "Maintenance" : r.RoleName,
                    Selected = r.Id == selectedRoleId
                })
                .ToList();
            return View(staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            var existingUser = await _staffService.GetStaffByIdAsync(user.Id);
            if (existingUser == null) return NotFound();

            existingUser.firstName = user.firstName;
            existingUser.lastName = user.lastName;
            existingUser.email = user.email;
            existingUser.username = user.username;

            var isActiveFormValues = Request.Form["isActive"].ToString();
            existingUser.isActive = isActiveFormValues.Contains("true");

            var success = await _staffService.UpdateStaffAsync(existingUser);
            if (success)
            {
                TempData["Success"] = "Staff profile updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Failed to update staff record.";
            ViewBag.Roles = _context.Roles
                .Where(r => r.RoleName != "Student")
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.RoleName == "Staff" ? "Maintenance" : r.RoleName
                })
                .ToList();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUserId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (id == currentUserId)
            {
                TempData["Error"] = "Safety Lock: You cannot terminate your own administrative account.";
                return RedirectToAction(nameof(Index));
            }

            var success = await _staffService.DeleteStaffAsync(id);
            if (success)
            {
                TempData["Success"] = "Staff record and dependencies successfully purged.";
            }
            else
            {
                TempData["Error"] = "Database Integrity Conflict: Failed to delete staff member.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

