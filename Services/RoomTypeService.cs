using DormMS.Web.Interfaces;
using DormMS.Web.Models;

namespace DormMS.Web.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _repo;
        public RoomTypeService(IRoomTypeRepository repo) { _repo = repo; }

        public async Task<IEnumerable<RoomType>> GetRoomTypesListAsync() => await _repo.GetAllAsync();
        public async Task CreateRoomTypeAsync(RoomType roomType) => await _repo.AddAsync(roomType);
    }
}