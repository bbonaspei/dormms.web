using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using DormMS.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DormMS.Web.Controllers
{
    [Route("Students/[action]/{id?}")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IDocumentService _documentService;
        private readonly IAuditService _audit;
        private readonly ApplicationDbContext _context;
        public StudentsController(IStudentService studentService, IDocumentService documentService, IAuditService audit, ApplicationDbContext context)
        {
            _studentService = studentService;
            _documentService = documentService;
            _audit = audit;
            _context = context;
        }

        [Authorize(Roles = "Admin,Manager,DormManager")]
        [HttpGet]
        public async Task<IActionResult> Index() => View(await _studentService.GetStudentListAsync());

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();

            if (!User.IsInRole("Admin") && !User.IsInRole("Manager") && !User.IsInRole("DormManager"))
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdString)) return Unauthorized();
                
                var userId = int.Parse(userIdString);
                if (student.userId != userId) return Forbid();
            }

            ViewBag.Documents = await _context.StudentDocuments.Where(d => d.studentId == id).ToListAsync();

            var activeAllocation = await _context.Allocations
                .Include(a => a.Room)
                .FirstOrDefaultAsync(a => a.studentId == id && a.isCurrent && (a.status == "Confirmed" || a.status == "Checked-In" || a.status == "Pending"));
            
            ViewBag.ActiveAllocation = activeAllocation;
            ViewBag.HasActiveAllocation = activeAllocation != null;
            return View(student);
        }

        [Authorize(Roles = "Admin,Manager,DormManager")]
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student, string firstName, string lastName, string email)
        {
            ModelState.Remove("User");
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users.AnyAsync(u => u.username == student.studentId);
                if (existingUser)
                {
                    ModelState.AddModelError("studentId", "Identity Conflict: This Student ID is already linked to an existing account.");
                }
                else
                {
                    await _studentService.EnrollNewStudentAsync(student, firstName, lastName, email);
                    TempData["Success"] = "Resident enrolled successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(student);
        }
        [Authorize(Roles = "Admin,Manager,DormManager")]
        [HttpGet]
        public async Task<IActionResult> Export(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();

            string content = $"STUDENT RECORD - {DateTime.Now}\n" +
                             $"==============================\n" +
                             $"Name: {student.User?.firstName} {student.User?.lastName}\n" +
                             $"ID Number: {student.studentId}\n" +
                             $"University: {student.university}\n" +
                             $"Department: {student.course}\n" +
                             $"Status: {student.status}\n";

            var bytes = System.Text.Encoding.UTF8.GetBytes(content);
            return File(bytes, "text/plain", $"{student.studentId}_record.txt");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.Include(s => s.User).FirstOrDefaultAsync(s => s.id == id);
            if (student == null) return NotFound();

            if (!User.IsInRole("Admin") && !User.IsInRole("Manager") && !User.IsInRole("DormManager"))
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

                var userId = int.Parse(userIdString);
                if (student.userId != userId) return Forbid();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student, string? newUsername)
        {
            if (id != student.id) return NotFound();

            bool isAdmin = User.IsInRole("Admin") || User.IsInRole("Manager") || User.IsInRole("DormManager");

            if (!isAdmin)
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

                var userId = int.Parse(userIdString);
                var existing = await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.id == id);
                if (existing?.userId != userId) return Forbid();
            }

            ModelState.Remove("User");
            if (ModelState.IsValid)
            {

                if (isAdmin && !string.IsNullOrEmpty(newUsername))
                {
                    var user = await _context.Users.FindAsync(student.userId);
                    if (user != null)
                    {
                        user.username = newUsername;
                        _context.Users.Update(user);
                    }
                }

                _context.Update(student);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Profile and credentials updated successfully!";
                return RedirectToAction(nameof(Details), new { id = student.id });
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadDoc(int studentId, string docType, IFormFile file)
        {

            if (!User.IsInRole("Admin") && !User.IsInRole("Manager") && !User.IsInRole("DormManager"))
            {
                var student = await _context.Students.FindAsync(studentId);
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdString)) return Unauthorized();
                
                var userId = int.Parse(userIdString);
                if (student?.userId != userId) return Forbid();
            }

            if (file != null && file.Length > 0)
            {

                var result = await _documentService.UploadDocumentAsync(studentId, docType, file);

                if (result)
                {

                    await _audit.LogActionAsync("UPLOAD", "Document", studentId, null, $"Document {docType} uploaded.");
                }
            }
            TempData["Success"] = "Document secured in repository.";
            return RedirectToAction("Details", new { id = studentId });
        }

        [HttpGet]
        public async Task<IActionResult> DownloadDoc(int id)
        {
            var document = await _context.StudentDocuments.Include(d => d.Student).FirstOrDefaultAsync(d => d.id == id);
            if (document == null) return NotFound();

            if (!User.IsInRole("Admin") && !User.IsInRole("Manager") && !User.IsInRole("DormManager"))
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

                var userId = int.Parse(userIdString);
                if (document.Student?.userId != userId) return Forbid();
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", document.filePath.TrimStart('/'));

            if (!System.IO.File.Exists(path)) return NotFound("File not found on server.");

            var bytes = await System.IO.File.ReadAllBytesAsync(path);
            return File(bytes, "application/octet-stream", document.documentName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager,DormManager")]
        public async Task<IActionResult> DeleteDoc(int id)
        {
            var document = await _context.StudentDocuments.FindAsync(id);
            if (document == null) return NotFound();

            int studentId = document.studentId;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", document.filePath.TrimStart('/'));
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _context.StudentDocuments.Remove(document);
            await _context.SaveChangesAsync();

            await _audit.LogActionAsync("DELETE", "Document", id, null, $"Document {document.documentName} deleted.");
            TempData["Success"] = "Document removed from repository.";

            return RedirectToAction("Details", new { id = studentId });
        }

        [Authorize(Roles = "Admin,DormManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.Include(s => s.User).FirstOrDefaultAsync(s => s.id == id);
            if (student == null) return NotFound();

            var userId = student.userId;

            try 
            {

                async Task TryCleanup(Func<Task> action, string label) {
                    try { await action(); } 
                    catch {  }
                }

                await TryCleanup(async () => {
                    var docs = _context.StudentDocuments.Where(d => d.studentId == id);
                    _context.StudentDocuments.RemoveRange(docs);
                    await _context.SaveChangesAsync();
                }, "Docs");

                await TryCleanup(async () => {
                    var allocations = _context.Allocations.Where(a => a.studentId == id);
                    _context.Allocations.RemoveRange(allocations);
                    await _context.SaveChangesAsync();
                }, "Allocations");

                await TryCleanup(async () => {
                    var maintenance = _context.MaintenanceRequests.Where(m => m.studentId == id);
                    _context.MaintenanceRequests.RemoveRange(maintenance);
                    await _context.SaveChangesAsync();
                }, "Maintenance");

                await TryCleanup(async () => {
                    var fees = _context.StudentFees.Where(f => f.studentId == id);
                    _context.StudentFees.RemoveRange(fees);
                    await _context.SaveChangesAsync();
                }, "Fees");

                await TryCleanup(async () => {
                    var payments = _context.Payments.Where(p => p.studentId == id);
                    _context.Payments.RemoveRange(payments);
                    await _context.SaveChangesAsync();
                }, "Payments");

                await TryCleanup(async () => {
                    var penalties = _context.Penalties.Where(p => p.studentId == id);
                    _context.Penalties.RemoveRange(penalties);
                    await _context.SaveChangesAsync();
                }, "Penalties");

                var notifications = _context.Notifications.Where(n => n.userId == userId);
                _context.Notifications.RemoveRange(notifications);

                var userRoles = _context.UserRoles.Where(ur => ur.UserId == userId);
                _context.UserRoles.RemoveRange(userRoles);

                _context.Students.Remove(student);
                
                var user = await _context.Users.FindAsync(userId);
                if (user != null) _context.Users.Remove(user);

                await _context.SaveChangesAsync();
                TempData["Success"] = "Resident profile and all historical data permanently removed.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Critical error during purge: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

