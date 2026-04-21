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

        // KESİN ÇÖZÜM: Kullanıcı "Students" tablosunda var mı diye bakıyoruz.
        private async Task<Student?> GetCurrentStudentAsync()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return null;

            int userId = int.Parse(userIdStr);
            return await _service.GetStudentProfileAsync(userId); // Eğer null dönerse öğrencidir DEĞİLDİR (Admindir).
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // 1. Giriş yapan kullanıcının User tablosundaki ID'sini alıyoruz
            var userIdStr = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
            int userId = string.IsNullOrEmpty(userIdStr) ? 0 : int.Parse(userIdStr);

            // 2. KARTLARI BOYAMAK VE BUTONLARI GİZLEMEK İÇİN BU ID'Yİ VIEW'A GÖNDERİYORUZ (KRİTİK)
            ViewBag.CurrentUserId = userId;

            // 3. Kullanıcı Öğrenci (Student) mi diye veritabanından kontrol ediyoruz
            var student = await GetCurrentStudentAsync();
            ViewBag.IsStudent = student != null;

            // 4. Öğrenciyse sadece kendi id'sini gönderir (sadece kendi işleri gelir). 
            // Admin veya Personel ise null gider (bütün işler gelir).
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

        [Authorize(Roles = "Admin,Manager,DormManager,Student")] // Staff can see archive but maybe not create? User said "create yapamaz"
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

                // 1. Öğrenci isimlerini ve numaralarını projeksiyon yapıyoruz ve isme göre sıralıyoruz
                var studentList = ((IEnumerable<Student>)data.Students)
                    .Select(s => new { 
                        id = s.id, 
                        fullName = $"{s.User?.firstName} {s.User?.lastName} ({s.studentId})" 
                    })
                    .OrderBy(s => s.fullName)
                    .ToList();

                // 2. "Admin / General Report" seçeneğini en başa ekliyoruz (studentId = null için)
                // SelectList'e null eklemek bazen sıkıntılı olabilir, o yüzden View tarafında manuel 'null' göndereceğiz
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

            // Eğer öğrenciyse formdaki gizli ID'lere güvenme, arkadan kendin bas (Güvenlik)
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
            return RedirectToAction(nameof(Create)); // Hata varsa GET'e geri yolla ki ViewBag'ler düzgün dolsun
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var request = await _service.GetRequestByIdAsync(id);
            if (request == null) return NotFound();

            var student = await GetCurrentStudentAsync();
            
            // SECURITY CHECK: If student, they can only view their own requests (or general reports)
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
        [Authorize(Roles = "Admin,Manager,Staff")] // Students cannot assign staff
        public async Task<IActionResult> Assign(int id, int staffId)
        {
            await _service.AssignStaffAsync(id, staffId);
            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int requestId, string newStatus)
        {
            var request = await _service.GetRequestByIdAsync(requestId);
            if (request == null) return NotFound();

            // SECURITY: Students cannot change status of ANY request
            if (User.IsInRole("Student"))
            {
                return Forbid();
            }

            // SECURITY CHECK: If Staff, they can only complete tasks ASSIGNED to them
            if (User.IsInRole("Staff") && !User.IsInRole("Admin"))
            {
                var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int userId = string.IsNullOrEmpty(userIdStr) ? 0 : int.Parse(userIdStr);
                if (request.assignedTo != userId) return Forbid();
                if (newStatus != "Completed") return Forbid(); // Staff can only mark as done
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
            
            // SECURITY CHECK: Students can only delete their own PENDING requests
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