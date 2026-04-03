using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IRoomTypeService
    {
        Task<IEnumerable<RoomType>> GetRoomTypesListAsync();
        Task CreateRoomTypeAsync(RoomType roomType);
    }
}