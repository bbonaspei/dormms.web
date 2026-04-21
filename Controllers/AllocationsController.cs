using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace DormMS.Web.Controllers
{
    [Authorize(Roles = "Admin,Manager,DormManager")]
    [Route("Allocations/[action]/{id?}")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class AllocationsController : Controller
    {
        private readonly IAllocationService _allocationService;
        private readonly IStudentService _studentService;
        private readonly IRoomService _roomService;
        private readonly IFinancialService _financialService;

        public AllocationsController(IAllocationService allocationService, IStudentService studentService, IRoomService roomService, IFinancialService financialService)
        {
            _allocationService = allocationService;
            _studentService = studentService;
            _roomService = roomService;
            _financialService = financialService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(await _allocationService.GetActiveAllocationsAsync());

        [HttpGet]
        public async Task<IActionResult> Create(int? studentId)
        {
            var students = await _studentService.GetStudentListAsync();
            var rooms = await _roomService.GetAvailableRoomsAsync();
            ViewBag.studentId = new SelectList(students, "id", "studentId", studentId);
            ViewBag.roomId = new SelectList(rooms.Where(r => r.status == "Available"), "id", "roomNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Allocation allocation)
        {
            var result = await _allocationService.CreateAllocationAsync(
                allocation.studentId, allocation.roomId, allocation.startDate,
                allocation.endDate ?? DateTime.Now.AddYears(1), allocation.securityDeposit, allocation.keyCardNumber ?? "");

            if (result)
            {
                // OTOMATİK BORÇLANDIRMA SİNKRONİZASYONU
                await _financialService.SyncStudentChargesAsync(allocation.studentId);
                TempData["Success"] = "Student assigned to unit and billing synchronized.";
                return RedirectToAction(nameof(Index));
            }
            return View(allocation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Terminate(int id)
        {
            await _allocationService.TerminateAllocationAsync(id);
            TempData["Success"] = "Residency terminated successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}