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
            room.status = "Available";
            room.currentOccupancy = 0;
            await _roomRepository.AddAsync(room);
        }

        // SONUNDA S TAKISI VAR (Interface ile aynı)
        public async Task<IEnumerable<Building>> GetBuildingsAsync()
        {
            return await _context.Buildings.ToListAsync();
        }

        public async Task<IEnumerable<RoomType>> GetRoomTypesAsync()
        {
            return await _context.RoomTypes.ToListAsync();
        }
        public async Task UpdateRoomAsync(Room room)
        {
            // Veritabanında güncelleme yapıyoruz
            await _roomRepository.UpdateAsync(room);
        }
        public async Task DeleteRoomAsync(int id)
        {
            await _roomRepository.DeleteAsync(id);
        }
    }
}