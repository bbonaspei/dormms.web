using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DormMS.Web.Interfaces;
using DormMS.Web.Models; // KRİTİK EKSİK BURASIYDI

namespace DormMS.Web.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _roomService.GetAvailableRoomsAsync();
            return View(rooms);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Servisten binaları ve tipleri çekiyoruz
            var buildings = await _roomService.GetBuildingsAsync();
            var roomTypes = await _roomService.GetRoomTypesAsync();

            ViewBag.buildingId = new SelectList(buildings, "id", "buildingName");
            ViewBag.roomTypeId = new SelectList(roomTypes, "id", "typeName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room)
        {
            ModelState.Remove("Building");
            ModelState.Remove("RoomType");

            if (ModelState.IsValid)
            {
                await _roomService.CreateRoomAsync(room);
                return RedirectToAction(nameof(Index));
            }

            // Eğer form hatalıysa sayfayı tekrar yüklerken ViewBag'leri unutmuyoruz
            var buildings = await _roomService.GetBuildingsAsync();
            var roomTypes = await _roomService.GetRoomTypesAsync();
            ViewBag.buildingId = new SelectList(buildings, "id", "buildingName", room.buildingId);
            ViewBag.roomTypeId = new SelectList(roomTypes, "id", "typeName", room.roomTypeId);

            return View(room);
        }
    }
}