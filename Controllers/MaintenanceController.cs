using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using DormMS.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly IMaintenanceService _service;
        private readonly ApplicationDbContext _context;

        public MaintenanceController(IMaintenanceService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task<IActionResult> Index() => View(await _service.GetAllRequestsAsync());

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Hangi öğrenci için hangi odada arıza kaydı açılacağını seçmek için listeleri gönderiyoruz
            var students = await _context.Students.Include(s => s.User).ToListAsync();
            var rooms = await _context.Rooms.ToListAsync();

            ViewBag.studentId = new SelectList(students, "id", "studentId");
            ViewBag.roomId = new SelectList(rooms, "id", "roomNumber");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MaintenanceRequest request)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateRequestAsync(request);
                return RedirectToAction(nameof(Index));
            }

            // Hata varsa listeleri tekrar yükle
            var students = await _context.Students.Include(s => s.User).ToListAsync();
            var rooms = await _context.Rooms.ToListAsync();
            ViewBag.studentId = new SelectList(students, "id", "studentId", request.studentId);
            ViewBag.roomId = new SelectList(rooms, "id", "roomNumber", request.roomId);

            return View(request);
        }
    }
}