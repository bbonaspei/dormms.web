using DormMS.Web.Interfaces;
using DormMS.Web.Models;

namespace DormMS.Web.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _repo;
        public RoomTypeService(IRoomTypeRepository repo) { _repo = repo; }

        public async Task<IEnumerable<RoomType>> GetRoomTypesListAsync() => await _repo.GetAllAsync();
        public async Task<RoomType?> GetRoomTypeByIdAsync(int id) => await _repo.GetByIdAsync(id);
        public async Task CreateRoomTypeAsync(RoomType roomType) => await _repo.AddAsync(roomType);
        public async Task UpdateRoomTypeAsync(RoomType roomType) => await _repo.UpdateAsync(roomType);
        public async Task DeleteRoomTypeAsync(int id) => await _repo.DeleteAsync(id);
        public async Task<IEnumerable<Fee>> GetAllFeesAsync() => await _repo.GetAllFeesAsync();
    }
}

