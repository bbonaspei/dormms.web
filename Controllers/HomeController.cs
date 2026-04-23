using DormMS.Web.Data;
using DormMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using DormMS.Web.Interfaces;

namespace DormMS.Web.Controllers
{
    [Route("Home/[action]/{id?}")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFinancialService _financialService;
        private readonly IRoomTypeService _roomTypeService;

        public HomeController(ApplicationDbContext context, IFinancialService financialService, IRoomTypeService roomTypeService)
        {
            _context = context;
            _financialService = financialService;
            _roomTypeService = roomTypeService;
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("~/")]
        [Route("~/Home")]
        [HttpGet]
        public async Task<IActionResult> Landing()
        {
            var roomTypes = await _roomTypeService.GetRoomTypesListAsync();
            return View(roomTypes);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString)) return RedirectToAction("Login", "Account");

            var userId = int.Parse(userIdString);

            if (User.IsInRole("Student") || User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Student"))
            {
                var student = await _context.Students
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.userId == userId);

                if (student == null)
                {
                    TempData["Error"] = "Resident profile record is missing. Please contact system administration.";
                    return View("StudentDashboard", new StudentDashboardViewModel { StudentFirstName = User.Identity.Name });
                }

                await _financialService.SyncStudentChargesAsync(student.id);

                var allocation = await _context.Allocations
                    .Include(a => a.Room).ThenInclude(r => r.Building)
                    .Include(a => a.Room).ThenInclude(r => r.RoomType)
                    .FirstOrDefaultAsync(a => a.studentId == student.id && a.isCurrent == true);

                var unpaidFees = await _context.StudentFees
                    .Where(f => f.studentId == student.id && f.status != "Paid")
                    .ToListAsync();

                decimal totalDue = unpaidFees.Sum(f => Convert.ToDecimal(f.amount));
                var nextDueFee = unpaidFees.OrderBy(f => f.dueDate).FirstOrDefault();

                int daysUntilDue = 0;
                bool isOverdue = false;

                if (nextDueFee != null)
                {
                    var timeSpan = Convert.ToDateTime(nextDueFee.dueDate).Date - DateTime.Today;
                    daysUntilDue = timeSpan.Days;
                    if (daysUntilDue < 0) isOverdue = true;
                }

                var activeRequestsCount = await _context.MaintenanceRequests
                    .CountAsync(m => m.studentId == student.id && (m.status == "Pending" || m.status == "In Progress"));

                var model = new StudentDashboardViewModel
                {
                    StudentFirstName = student.User?.firstName ?? "Ã–ÄŸrenci",
                    HasActiveLease = allocation != null,
                    RoomNumber = allocation?.Room?.roomNumber ?? "Not Assigned",
                    BuildingName = allocation?.Room?.Building?.buildingName ?? "N/A",
                    Floor = allocation?.Room?.floorNumber,
                    RoomType = allocation?.Room?.RoomType?.typeName ?? "N/A",
                    CheckInDate = allocation != null ? Convert.ToDateTime(allocation.startDate) : null,
                    LeaseEndDate = allocation?.endDate,
                    TotalUpcomingDue = totalDue,
                    DaysUntilDue = Math.Abs(daysUntilDue),
                    IsOverdue = isOverdue,
                    ActiveRequestCount = activeRequestsCount,
                    ProfilePicture = student.User?.profilePicture
                };

                return View("StudentDashboard", model);
            }

            if (User.IsInRole("Staff") || User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Staff"))
            {
                var activeTasks = await _context.MaintenanceRequests
                    .Include(m => m.Room)
                    .Include(m => m.Student).ThenInclude(s => s.User)
                    .Where(m => m.status == "Pending" || m.status == "In Progress")
                    .OrderByDescending(m => m.assignedTo == userId) 
                    .ThenByDescending(m => m.requestDate)
                    .ToListAsync();

                ViewBag.AllTasks = activeTasks;
                ViewBag.CurrentUserId = userId;
                ViewBag.StaffName = User.FindFirstValue("FullName") ?? User.Identity.Name;

                var totalSystemRequests = await _context.MaintenanceRequests.CountAsync();
                var myCompletedTasks = await _context.MaintenanceRequests.CountAsync(m => m.assignedTo == userId && m.status == "Completed");
                
                ViewBag.EfficiencyRate = totalSystemRequests > 0 
                    ? (int)Math.Round((double)myCompletedTasks / totalSystemRequests * 100) 
                    : 0;

                return View("StaffDashboard");
            }

            if (User.IsInRole("Admin") || User.IsInRole("Manager") || User.IsInRole("DormManager") || User.IsInRole("Finance") ||
                User.Claims.Any(c => c.Type == ClaimTypes.Role && (c.Value == "Admin" || c.Value == "Manager" || c.Value == "DormManager" || c.Value == "Finance")))
            {
                ViewBag.TotalRooms = await _context.Rooms.CountAsync(r => r.status != "Archived");
                ViewBag.ActiveStudents = await _context.Students.CountAsync(s => s.status == "Active");

                var unpaid = await _context.StudentFees.Where(f => f.status != "Paid").SumAsync(f => (decimal?)f.amount);
                ViewBag.UnpaidAmount = unpaid ?? 0;

                ViewBag.PendingRequestsCount = await _context.MaintenanceRequests.CountAsync(r => r.status == "Pending" || r.status == "In Progress");

                var studentsWithDocs = await _context.StudentDocuments.Select(d => d.studentId).Distinct().ToListAsync();
                var studentsMissingDocs = await _context.Students
                    .Include(s => s.User)
                    .Where(s => !studentsWithDocs.Contains(s.id))
                    .Take(10)
                    .ToListAsync();
                
                ViewBag.MissingDocCount = await _context.Students.CountAsync(s => !studentsWithDocs.Contains(s.id));
                ViewBag.StudentsMissingDocs = studentsMissingDocs;

                var topDebtors = await _context.StudentFees
                    .Include(f => f.Student).ThenInclude(s => s.User)
                    .Where(f => f.status != "Paid")
                    .GroupBy(f => f.studentId)
                    .Select(g => new {
                        Student = g.First().Student,
                        TotalAmount = g.Sum(f => f.amount)
                    })
                    .OrderByDescending(x => x.TotalAmount)
                    .Take(5).ToListAsync();
                
                ViewBag.TopDebtors = topDebtors.Select(x => new StudentFee {
                    Student = x.Student,
                    amount = x.TotalAmount
                }).ToList();

                ViewBag.AllTasks = await _context.MaintenanceRequests
                    .Include(m => m.Room)
                    .Include(m => m.Student).ThenInclude(s => s.User)
                    .Where(m => m.status == "Pending" || m.status == "In Progress")
                    .OrderByDescending(m => m.requestDate)
                    .Take(10).ToListAsync();

                ViewBag.StaffName = User.FindFirstValue("FullName") ?? User.Identity.Name;

                ViewBag.RecentPayments = await _context.Payments
                    .Include(p => p.Student).ThenInclude(s => s.User)
                    .OrderByDescending(p => p.paymentDate)
                    .Take(5).ToListAsync();

                var last7Days = Enumerable.Range(0, 7)
                    .Select(i => DateTime.Today.AddDays(-i))
                    .OrderBy(d => d)
                    .ToList();

                var revenueData = new List<decimal>();
                var revenueLabels = new List<string>();

                foreach (var day in last7Days)
                {
                    var dayTotal = await _context.Payments
                        .Where(p => p.paymentDate.Date == day.Date)
                        .SumAsync(p => (decimal?)p.amount) ?? 0;
                    
                    revenueData.Add(dayTotal);
                    revenueLabels.Add(day.ToString("ddd")); 
                }

                ViewBag.RevenueData = revenueData;
                ViewBag.RevenueLabels = revenueLabels;

                if (User.IsInRole("Finance") && !User.IsInRole("Admin") && !User.IsInRole("Manager"))
                {
                    return View("FinanceDashboard");
                }

                return View(); 
            }

            return View(); 
        }

        [HttpGet]
        public async Task<IActionResult> GetMyNotifications()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString)) return Json(new List<object>());

            var userId = int.Parse(userIdString);
            var notes = await _context.Notifications
                .Where(n => n.userId == userId && !n.isRead)
                .OrderByDescending(n => n.createdAt)
                .ToListAsync();

            return Json(notes);
        }

        [HttpPost]
        public async Task<IActionResult> MarkNotificationAsRead(int id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

            var userId = int.Parse(userIdString);
            var note = await _context.Notifications.FirstOrDefaultAsync(n => n.id == id && n.userId == userId);
            
            if (note != null)
            {
                note.isRead = true;
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager,Finance")]
        public async Task<IActionResult> SyncAllFinances()
        {
            var students = await _context.Students.Where(s => s.status == "Active").ToListAsync();
            foreach (var student in students)
            {
                await _financialService.SyncStudentChargesAsync(student.id);
            }
            TempData["Success"] = "Global financial audit completed. All resident balances synchronized.";
            return RedirectToAction(nameof(Index));
        }
    }
}

