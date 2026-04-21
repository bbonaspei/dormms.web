using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;

namespace DormMS.Web.Controllers
{
    [Authorize]
    [Route("Rooms/[action]/{id?}")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class RoomsController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IAuditService _audit;
        private readonly IAllocationService _allocationService;
        private readonly IStudentService _studentService;
        private readonly INotificationService _notificationService;

        public RoomsController(IRoomService roomService, IAuditService audit, IAllocationService allocationService, IStudentService studentService, INotificationService notificationService)
        {
            _roomService = roomService;
            _audit = audit;
            _allocationService = allocationService;
            _studentService = studentService;
            _notificationService = notificationService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestRoom(int roomId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

            var student = await _studentService.GetStudentByUserIdAsync(int.Parse(userIdString));
            if (student == null)
            {
                TempData["Error"] = "Only registered residents can request rooms.";
                return RedirectToAction(nameof(Browse));
            }

            var success = await _allocationService.RequestAllocationAsync(student.id, roomId);
            if (success)
            {
                TempData["Success"] = "Your booking request has been submitted and is awaiting approval.";
                // Notify admins? (In a real app, you'd find admins and notify them)
            }
            else
            {
                TempData["Error"] = "You already have an active or pending room allocation.";
            }

            return RedirectToAction(nameof(Browse));
        }


        [Authorize(Roles = "Admin,Manager,DormManager")]
        [HttpGet]
        public async Task<IActionResult> Index() => View((await _roomService.GetAvailableRoomsAsync()).Where(r => r.status != "Archived"));

        [Authorize(Roles = "Admin,Manager,DormManager")]
        [HttpGet]
        public async Task<IActionResult> Archives() => View((await _roomService.GetAvailableRoomsAsync()).Where(r => r.status == "Archived"));

        [HttpGet]
        public async Task<IActionResult> Browse()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userIdString))
            {
                var student = await _studentService.GetStudentByUserIdAsync(int.Parse(userIdString));
                if (student != null)
                {
                    ViewBag.HasActiveAllocation = await _allocationService.GetStudentActiveAllocationAsync(student.id) != null;
                }
            }

            var availableRooms = (await _roomService.GetAvailableRoomsAsync()).Where(r => r.status == "Available");
            return View(availableRooms);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var room = await _roomService.GetRoomDetailsAsync(id.Value);
            return room == null ? NotFound() : View(room);
        }

        [Authorize(Roles = "Admin,Manager,DormManager")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.buildingId = new SelectList(await _roomService.GetBuildingsAsync(), "id", "buildingName");
            ViewBag.roomTypeId = new SelectList(await _roomService.GetRoomTypesAsync(), "id", "typeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room)
        {
            ModelState.Remove("Building"); ModelState.Remove("RoomType");
            if (ModelState.IsValid)
            {
                await _roomService.CreateRoomAsync(room);
                await _audit.LogActionAsync("CREATE", "Room", room.id, null, $"New room {room.roomNumber} registered");
                TempData["Success"] = "New unit added to inventory!";
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        [Authorize(Roles = "Admin,Manager,DormManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Archive(int id)
        {
            var room = await _roomService.GetRoomDetailsAsync(id);
            if (room != null)
            {
                room.status = "Archived"; await _roomService.UpdateRoomAsync(room);
                await _audit.LogActionAsync("DELETE", "Room", id, "Available", "Moved to archive");
            }
            TempData["Success"] = "Room moved to archives.";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Manager,DormManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(int id)
        {
            var room = await _roomService.GetRoomDetailsAsync(id);
            if (room != null)
            {
                room.status = "Available"; await _roomService.UpdateRoomAsync(room);
                await _audit.LogActionAsync("UPDATE", "Room", id, "Archived", "Restored to inventory");
            }
            TempData["Success"] = "Room configuration updated.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Rooms/Edit/5
        [Authorize(Roles = "Admin,Manager,DormManager")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var room = await _roomService.GetRoomDetailsAsync(id.Value);
            if (room == null) return NotFound();

            // Dropdown listelerini (Binalar ve Tipler) dolduruyoruz
            ViewBag.buildingId = new SelectList(await _roomService.GetBuildingsAsync(), "id", "buildingName", room.buildingId);
            ViewBag.roomTypeId = new SelectList(await _roomService.GetRoomTypesAsync(), "id", "typeName", room.roomTypeId);

            return View(room);
        }

        // POST: Rooms/Edit/5
        [Authorize(Roles = "Admin,Manager,DormManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Room room)
        {
            if (id != room.id) return NotFound();

            // İlişkili nesneleri doğrulamadan muaf tut (Hata almamak için)
            ModelState.Remove("Building");
            ModelState.Remove("RoomType");

            if (ModelState.IsValid)
            {
                await _roomService.UpdateRoomAsync(room);

                // AUDIT LOG: Değişikliği mühürle (Rapor Sayfa 22)
                await _audit.LogActionAsync("UPDATE", "Room", room.id, null, $"Room {room.roomNumber} configuration updated.");

                TempData["Success"] = "Room configuration updated.";
                return RedirectToAction(nameof(Index));
            }

            // Hata varsa listeleri tekrar yükle
            ViewBag.buildingId = new SelectList(await _roomService.GetBuildingsAsync(), "id", "buildingName", room.buildingId);
            ViewBag.roomTypeId = new SelectList(await _roomService.GetRoomTypesAsync(), "id", "typeName", room.roomTypeId);
            return View(room);
        }
    }
}