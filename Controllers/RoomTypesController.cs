using Microsoft.AspNetCore.Mvc;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;

namespace DormMS.Web.Controllers
{
    [Route("RoomTypes/[action]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class RoomTypesController : Controller
    {
        private readonly IRoomTypeService _roomTypeService;
        private readonly IAuditService _audit;

        public RoomTypesController(IRoomTypeService roomTypeService, IAuditService audit)
        {
            _roomTypeService = roomTypeService;
            _audit = audit;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var types = await _roomTypeService.GetRoomTypesListAsync();
            return View(types);
        }

        [HttpGet]
        public IActionResult Landing() => View();

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                await _roomTypeService.CreateRoomTypeAsync(roomType);

                // AUDIT LOG: Yeni oda tipi
                await _audit.LogActionAsync("CREATE", "RoomType", roomType.id, null, $"Room Type {roomType.typeName} defined.");

                return RedirectToAction(nameof(Index));
            }
            return View(roomType);
        }
    }
}