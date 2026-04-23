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
            var buildings = await _roomService.GetBuildingsAsync();
            var roomTypes = await _roomService.GetRoomTypesAsync();

            ViewBag.BuildingsJson = System.Text.Json.JsonSerializer.Serialize(buildings.Select(b => new { b.id, b.totalFloors }));
            ViewBag.RoomTypesJson = System.Text.Json.JsonSerializer.Serialize(roomTypes.Select(rt => new { rt.id, rt.capacity, rt.hasBathroom }));

            ViewBag.buildingId = new SelectList(buildings, "id", "buildingName");
            ViewBag.roomTypeId = new SelectList(roomTypes, "id", "typeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room)
        {
            ModelState.Remove("Building"); ModelState.Remove("RoomType");

            var buildings = await _roomService.GetBuildingsAsync();
            var building = buildings.FirstOrDefault(b => b.id == room.buildingId);

            if (building == null)
            {
                ModelState.AddModelError("buildingId", "Selected building is inactive or does not exist. Please choose an active building.");
            }
            else if (room.floorNumber > building.totalFloors)
            {
                ModelState.AddModelError("floorNumber", $"Selected building only has {building.totalFloors} floors.");
            }

            var roomTypes = await _roomService.GetRoomTypesAsync();
            var roomType = roomTypes.FirstOrDefault(rt => rt.id == room.roomTypeId);
            if (roomType != null)
            {
                if (room.capacity != roomType.capacity)
                    ModelState.AddModelError("capacity", $"Capacity must be {roomType.capacity} for this room type.");
            }

            if (ModelState.IsValid)
            {
                try 
                {
                    await _roomService.CreateRoomAsync(room);
                    await _audit.LogActionAsync("CREATE", "Room", room.id, null, $"New room {room.roomNumber} registered");
                    TempData["Success"] = "New unit added to inventory!";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("roomNumber", ex.Message);
                }
            }

            ViewBag.BuildingsJson = System.Text.Json.JsonSerializer.Serialize(buildings.Select(b => new { b.id, b.totalFloors }));
            ViewBag.RoomTypesJson = System.Text.Json.JsonSerializer.Serialize(roomTypes.Select(rt => new { rt.id, rt.capacity, rt.hasBathroom }));
            ViewBag.buildingId = new SelectList(buildings, "id", "buildingName", room.buildingId);
            ViewBag.roomTypeId = new SelectList(roomTypes, "id", "typeName", room.roomTypeId);
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

                if (room.currentOccupancy.HasValue && room.currentOccupancy.Value > 0)
                {
                    TempData["Error"] = $"Cannot archive room {room.roomNumber} because it currently has {room.currentOccupancy} resident(s).";
                    return RedirectToAction(nameof(Index));
                }

                room.status = "Archived"; 
                await _roomService.UpdateRoomAsync(room);
                await _audit.LogActionAsync("DELETE", "Room", id, "Available", "Moved to archive");
                TempData["Success"] = "Room moved to archives.";
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Manager,DormManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _roomService.GetRoomDetailsAsync(id);
            if (room != null)
            {

                if (room.currentOccupancy.HasValue && room.currentOccupancy.Value > 0)
                {
                    TempData["Error"] = $"Cannot delete room {room.roomNumber} because it is currently occupied.";
                    return RedirectToAction(room.status == "Archived" ? nameof(Archives) : nameof(Index));
                }

                await _roomService.DeleteRoomAsync(id);
                await _audit.LogActionAsync("DELETE", "Room", id, null, "Permanently deleted from database");
                TempData["Success"] = "Room deleted permanently.";
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Manager,DormManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sync()
        {
            await _roomService.SyncAllRoomsOccupancyAsync();
            TempData["Success"] = "All room occupancy levels synchronized with active allocations.";
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

        [Authorize(Roles = "Admin,Manager,DormManager")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var room = await _roomService.GetRoomDetailsAsync(id.Value);
            if (room == null) return NotFound();

            var buildings = await _roomService.GetBuildingsAsync();
            var roomTypes = await _roomService.GetRoomTypesAsync();

            ViewBag.BuildingsJson = System.Text.Json.JsonSerializer.Serialize(buildings.Select(b => new { b.id, b.totalFloors }));
            ViewBag.RoomTypesJson = System.Text.Json.JsonSerializer.Serialize(roomTypes.Select(rt => new { rt.id, rt.capacity, rt.hasBathroom }));

            ViewBag.buildingId = new SelectList(buildings, "id", "buildingName", room.buildingId);
            ViewBag.roomTypeId = new SelectList(roomTypes, "id", "typeName", room.roomTypeId);

            return View(room);
        }

        [Authorize(Roles = "Admin,Manager,DormManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Room room)
        {
            if (id != room.id) return NotFound();

            ModelState.Remove("Building");
            ModelState.Remove("RoomType");

            var buildings = await _roomService.GetBuildingsAsync();
            var building = buildings.FirstOrDefault(b => b.id == room.buildingId);

            if (building == null)
            {
                ModelState.AddModelError("buildingId", "Selected building is inactive or does not exist. Please choose an active building.");
            }
            else if (room.floorNumber > building.totalFloors)
            {
                ModelState.AddModelError("floorNumber", $"Selected building only has {building.totalFloors} floors.");
            }

            var roomTypes = await _roomService.GetRoomTypesAsync();
            var roomType = roomTypes.FirstOrDefault(rt => rt.id == room.roomTypeId);
            if (roomType != null)
            {
                if (room.capacity != roomType.capacity)
                    ModelState.AddModelError("capacity", $"Capacity must be {roomType.capacity} for this room type.");
            }

            if (ModelState.IsValid)
            {
                try 
                {
                    var existingRoom = await _roomService.GetRoomDetailsAsync(id);
                    if (existingRoom == null) return NotFound();

                    if (room.status == "Archived" && existingRoom.currentOccupancy.HasValue && existingRoom.currentOccupancy.Value > 0)
                    {
                        ModelState.AddModelError("status", "Cannot archive occupied rooms. Please deallocate residents first.");
                    }

                    if (ModelState.IsValid)
                    {
                        existingRoom.roomNumber = room.roomNumber;
                        existingRoom.buildingId = room.buildingId;
                        existingRoom.roomTypeId = room.roomTypeId;
                        existingRoom.floorNumber = room.floorNumber;
                        existingRoom.capacity = room.capacity;
                        existingRoom.status = room.status;
                        existingRoom.hasBathroom = room.hasBathroom;
                        existingRoom.notes = room.notes;
                        existingRoom.updatedAt = DateTime.Now;

                        await _roomService.UpdateRoomAsync(existingRoom);
                        await _audit.LogActionAsync("UPDATE", "Room", room.id, null, $"Room {room.roomNumber} configuration updated.");

                        TempData["Success"] = "Room configuration updated.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("roomNumber", ex.Message);
                }
            }

            ViewBag.BuildingsJson = System.Text.Json.JsonSerializer.Serialize(buildings.Select(b => new { b.id, b.totalFloors }));
            ViewBag.RoomTypesJson = System.Text.Json.JsonSerializer.Serialize(roomTypes.Select(rt => new { rt.id, rt.capacity }));
            ViewBag.buildingId = new SelectList(buildings, "id", "buildingName", room.buildingId);
            ViewBag.roomTypeId = new SelectList(roomTypes, "id", "typeName", room.roomTypeId);
            return View(room);
        }
    }
}

