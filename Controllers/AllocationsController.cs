using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;

namespace DormMS.Web.Controllers
{
    public class AllocationsController : Controller
    {
        private readonly IAllocationService _allocationService;
        private readonly IStudentService _studentService;
        private readonly IRoomService _roomService;

        public AllocationsController(IAllocationService allocationService, IStudentService studentService, IRoomService roomService)
        {
            _allocationService = allocationService;
            _studentService = studentService;
            _roomService = roomService;
        }

        public async Task<IActionResult> Index()
        {
            var allocations = await _allocationService.GetActiveAllocationsAsync();
            return View(allocations);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Atama yapabilmek için öğrenci ve oda listesini gönderiyoruz
            var students = await _studentService.GetStudentListAsync();
            var rooms = await _roomService.GetAvailableRoomsAsync();

            ViewBag.studentId = new SelectList(students, "id", "studentId");
            ViewBag.roomId = new SelectList(rooms, "id", "roomNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Allocation allocation)
        {
            // Servisteki yeni metodu, diyagramdaki parametrelerle çağırıyoruz
            var result = await _allocationService.CreateAllocationAsync(
                allocation.studentId,
                allocation.roomId,
                allocation.startDate,
                allocation.endDate ?? DateTime.Now.AddYears(1)
            );

            if (result) return RedirectToAction(nameof(Index));

            return View(allocation);
        }
    }
}