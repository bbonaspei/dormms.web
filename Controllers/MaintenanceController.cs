using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DormMS.Web.Controllers
{
    [Authorize]
    [Route("Maintenance/[action]")]
    public class MaintenanceController : Controller
    {
        private readonly IMaintenanceService _service;
        private readonly IAuditService _audit;

        public MaintenanceController(IMaintenanceService service, IAuditService audit)
        {
            _service = service;
            _audit = audit;
        }

        private async Task<Student?> GetCurrentStudentAsync()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return null;

            int userId = int.Parse(userIdStr);
            return await _service.GetStudentProfileAsync(userId);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var userIdStr = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
            int userId = string.IsNullOrEmpty(userIdStr) ? 0 : int.Parse(userIdStr);

            ViewBag.CurrentUserId = userId;

            var student = await GetCurrentStudentAsync();
            ViewBag.IsStudent = student != null;

            var activeRequests = await _service.GetActiveRequestsAsync(student?.id);

            return View(activeRequests);
        }

        [HttpGet]
        public async Task<IActionResult> Archive()
        {
            var student = await GetCurrentStudentAsync();
            var archivedRequests = await _service.GetArchivedRequestsAsync(student?.id);

            ViewBag.IsStudent = student != null;
            return View(archivedRequests);
        }

        [Authorize(Roles = "Admin,Manager,DormManager,Student")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var student = await GetCurrentStudentAsync();
            bool isStudent = student != null;
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();
            int userId = int.Parse(userIdStr);

            var data = await _service.GetCreateDropdownDataAsync(userId, isStudent);

            if (isStudent)
            {
                ViewBag.IsStudent = true;
                ViewBag.StudentId = data.Student?.id;
                ViewBag.RoomId = data.Room?.id;
                ViewBag.StudentName = $"{data.Student?.User?.firstName} {data.Student?.User?.lastName}";
                ViewBag.RoomNumber = data.Room?.roomNumber ?? "No Active Room";
            }
            else
            {
                ViewBag.IsStudent = false;

                var studentList = ((IEnumerable<Student>)data.Students)
                    .Select(s => new { 
                        id = s.id, 
                        fullName = $"{s.User?.firstName} {s.User?.lastName} ({s.studentId})" 
                    })
                    .OrderBy(s => s.fullName)
                    .ToList();

                var studentRoomMap = new Dictionary<int, int?>();
                foreach (var s in (IEnumerable<Student>)data.Students)
                {
                    var room = await _service.GetStudentRoomAsync(s.id);
                    studentRoomMap[s.id] = room?.id;
                }
                ViewBag.StudentRoomMapJson = System.Text.Json.JsonSerializer.Serialize(studentRoomMap);

                ViewBag.studentId = new SelectList(studentList, "id", "fullName");
                ViewBag.roomId = new SelectList(data.Rooms, "id", "roomNumber");
            }

            return View();
        }

        [Authorize(Roles = "Admin,Manager,DormManager,Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MaintenanceRequest request)
        {
            ModelState.Remove("Student");
            ModelState.Remove("Room");
            ModelState.Remove("Staff");

            var student = await GetCurrentStudentAsync();

            if (student != null)
            {
                var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

                int userId = int.Parse(userIdStr);
                var data = await _service.GetCreateDropdownDataAsync(userId, true);
                request.studentId = data.Student?.id ?? 0;
                request.roomId = data.Room?.id ?? 0;
            }

            if (ModelState.IsValid)
            {
                await _service.CreateRequestAsync(request);
                await _audit.LogActionAsync("CREATE", "Maintenance", request.id, null, $"Request #{request.requestNumber} opened.");
                TempData["Success"] = "Maintenance request has been broadcasted successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Please correct the errors in the form.";
            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var request = await _service.GetRequestByIdAsync(id);
            if (request == null) return NotFound();

            var student = await GetCurrentStudentAsync();

            if (student != null && request.studentId.HasValue && request.studentId.Value != student.id)
            {
                return Forbid();
            }

            ViewBag.IsStudent = student != null;

            var staffList = await _service.GetAvailableStaffAsync();
            ViewBag.StaffList = new SelectList(staffList, "Id", "firstName");

            return View(request);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<IActionResult> Assign(int id, int staffId)
        {
            if (staffId <= 0)
            {
                TempData["Error"] = "Please select a technician before assigning.";
                return RedirectToAction(nameof(Details), new { id = id });
            }
            
            bool success = await _service.AssignStaffAsync(id, staffId);
            if (!success)
            {
                TempData["Error"] = "Assignment failed. The selected staff might be inactive or unavailable.";
                return RedirectToAction(nameof(Details), new { id = id });
            }

            TempData["Success"] = "Technician assigned and task status updated.";
            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int requestId, string newStatus)
        {
            var request = await _service.GetRequestByIdAsync(requestId);
            if (request == null) return NotFound();

            if (User.IsInRole("Student"))
            {
                return Forbid();
            }

            if (User.IsInRole("Staff") && !User.IsInRole("Admin"))
            {
                var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int userId = string.IsNullOrEmpty(userIdStr) ? 0 : int.Parse(userIdStr);
                if (request.assignedTo != userId) return Forbid();
                if (newStatus != "Completed") return Forbid();

                if (request.Staff != null && !request.Staff.isActive)
                {
                    TempData["Error"] = "Access Denied. Your staff account is currently inactive and cannot perform operations.";
                    return RedirectToAction(nameof(Index));
                }
            }

            await _service.UpdateStatusAsync(requestId, newStatus);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Manager,DormManager,Student")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var request = await _service.GetRequestByIdAsync(id);
            if (request == null) return NotFound();

            var student = await GetCurrentStudentAsync();

            if (student != null)
            {
                if (request.studentId != student.id || request.status != "Pending") return Forbid();
            }

            await _service.DeleteRequestAsync(id);
            TempData["Success"] = "Request deleted permanently.";
            return RedirectToAction(nameof(Index));
        }
    }
}

