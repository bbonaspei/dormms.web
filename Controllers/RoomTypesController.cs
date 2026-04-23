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
        public async Task<IActionResult> Create()
        {
            ViewBag.Fees = await _roomTypeService.GetAllFeesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomType roomType)
        {
            if (ModelState.IsValid)
            {

                if (roomType.feeId.HasValue)
                {
                    var fees = await _roomTypeService.GetAllFeesAsync();
                    var selectedFee = fees.FirstOrDefault(f => f.id == roomType.feeId.Value);
                    if (selectedFee != null) roomType.basePrice = selectedFee.amount;
                }

                await _roomTypeService.CreateRoomTypeAsync(roomType);

                await _audit.LogActionAsync("CREATE", "RoomType", roomType.id, null, $"Room Type {roomType.typeName} defined.");

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Fees = await _roomTypeService.GetAllFeesAsync();
            return View(roomType);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var roomType = await _roomTypeService.GetRoomTypeByIdAsync(id);
            if (roomType == null) return NotFound();

            ViewBag.Fees = await _roomTypeService.GetAllFeesAsync();
            return View(roomType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoomType roomType)
        {
            if (ModelState.IsValid)
            {

                if (roomType.feeId.HasValue)
                {
                    var fees = await _roomTypeService.GetAllFeesAsync();
                    var selectedFee = fees.FirstOrDefault(f => f.id == roomType.feeId.Value);
                    if (selectedFee != null) roomType.basePrice = selectedFee.amount;
                }

                await _roomTypeService.UpdateRoomTypeAsync(roomType);
                await _audit.LogActionAsync("UPDATE", "RoomType", roomType.id, null, $"Room Type {roomType.typeName} updated.");
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Fees = await _roomTypeService.GetAllFeesAsync();
            return View(roomType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var roomType = await _roomTypeService.GetRoomTypeByIdAsync(id);
            if (roomType != null)
            {
                await _roomTypeService.DeleteRoomTypeAsync(id);
                await _audit.LogActionAsync("DELETE", "RoomType", id, roomType.typeName, "Room Type permanently deleted.");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

