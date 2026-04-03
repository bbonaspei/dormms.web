using Microsoft.AspNetCore.Mvc;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;

namespace DormMS.Web.Controllers
{
    public class RoomTypesController : Controller
    {
        private readonly IRoomTypeService _roomTypeService;
        public RoomTypesController(IRoomTypeService roomTypeService) { _roomTypeService = roomTypeService; }

        public async Task<IActionResult> Index()
        {
            var types = await _roomTypeService.GetRoomTypesListAsync();
            return View(types);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                await _roomTypeService.CreateRoomTypeAsync(roomType);
                return RedirectToAction(nameof(Index));
            }
            return View(roomType);
        }
    }
}