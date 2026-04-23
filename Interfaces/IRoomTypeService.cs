using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IRoomTypeService
    {
        Task<IEnumerable<RoomType>> GetRoomTypesListAsync();
        Task<RoomType?> GetRoomTypeByIdAsync(int id);
        Task CreateRoomTypeAsync(RoomType roomType);
        Task UpdateRoomTypeAsync(RoomType roomType);
        Task DeleteRoomTypeAsync(int id);
        Task<IEnumerable<Fee>> GetAllFeesAsync();
    }
}

