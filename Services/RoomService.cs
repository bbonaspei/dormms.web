using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ApplicationDbContext _context;

        public RoomService(IRoomRepository roomRepository, ApplicationDbContext context)
        {
            _roomRepository = roomRepository;
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync()
        {
            return await _roomRepository.GetAllRoomsAsync();
        }

        public async Task<Room> GetRoomDetailsAsync(int id)
        {
            return await _roomRepository.GetByIdAsync(id);
        }

        public async Task CreateRoomAsync(Room room)
        {

            var exists = await _context.Rooms.AnyAsync(r => r.buildingId == room.buildingId && r.roomNumber == room.roomNumber);
            if (exists)
            {
                throw new InvalidOperationException($"Room number {room.roomNumber} already exists in this building.");
            }

            if (room.roomTypeId.HasValue)
            {
                var roomType = await _context.RoomTypes.FindAsync(room.roomTypeId.Value);
                if (roomType != null)
                {
                    room.hasBathroom = roomType.hasBathroom;
                    room.capacity = roomType.capacity;
                }
            }

            room.status = "Available";
            room.currentOccupancy = 0;
            await _roomRepository.AddAsync(room);
        }

        public async Task<IEnumerable<Building>> GetBuildingsAsync()
        {
            return await _context.Buildings
                .Where(b => b.status == "Active")
                .ToListAsync();
        }

        public async Task<IEnumerable<RoomType>> GetRoomTypesAsync()
        {
            return await _context.RoomTypes.ToListAsync();
        }
        public async Task UpdateRoomAsync(Room room)
        {

            var exists = await _context.Rooms.AnyAsync(r => r.buildingId == room.buildingId && r.roomNumber == room.roomNumber && r.id != room.id);
            if (exists)
            {
                throw new InvalidOperationException($"Room number {room.roomNumber} already exists in this building.");
            }

            if (room.roomTypeId.HasValue)
            {
                var roomType = await _context.RoomTypes.FindAsync(room.roomTypeId.Value);
                if (roomType != null)
                {
                    room.hasBathroom = roomType.hasBathroom;
                    room.capacity = roomType.capacity;
                }
            }

            room.currentOccupancy = await _context.Allocations
                .CountAsync(a => a.roomId == room.id && a.isCurrent && (a.status == "Confirmed" || a.status == "Checked-In"));

            if (room.status != "Archived" && room.status != "Maintenance")
            {

                if (room.currentOccupancy >= (room.capacity ?? 0))
                {
                    room.status = "Occupied";
                }
                else if (room.status != "Occupied")
                {
                    room.status = "Available";
                }
            }

            await _roomRepository.UpdateAsync(room);
        }
        public async Task DeleteRoomAsync(int id)
        {
            await _roomRepository.DeleteAsync(id);
        }

        public async Task SyncAllRoomsOccupancyAsync()
        {
            var rooms = await _context.Rooms.ToListAsync();
            foreach (var room in rooms)
            {
                room.currentOccupancy = await _context.Allocations
                    .CountAsync(a => a.roomId == room.id && a.isCurrent && (a.status == "Confirmed" || a.status == "Checked-In"));

                if (room.status != "Archived" && room.status != "Maintenance")
                {
                    if (room.currentOccupancy >= (room.capacity ?? 0))
                    {
                        room.status = "Occupied";
                    }
                    else if (room.status != "Occupied")
                    {
                        room.status = "Available";
                    }
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}

