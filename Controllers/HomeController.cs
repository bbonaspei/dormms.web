using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DormMS.Web.Data;

namespace DormMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Veritabanýndan gerçek sayýlarý alýyoruz
            ViewBag.TotalRooms = await _context.Rooms.CountAsync();
            ViewBag.OccupiedRooms = await _context.Rooms.SumAsync(r => r.currentOccupancy);

            // Arkadaþýnýn diyagramýndaki tablolardan verileri çekelim
            ViewBag.TotalStudents = await _context.Students.CountAsync();

            return View();
        }

        public IActionResult Landing()
        {
            return View();
        }
    }
}