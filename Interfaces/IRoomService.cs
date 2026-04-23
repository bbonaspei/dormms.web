using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IRoomService
    {

        Task<IEnumerable<Room>> GetAvailableRoomsAsync();
        Task<Room> GetRoomDetailsAsync(int id);

        Task CreateRoomAsync(Room room);
        Task UpdateRoomAsync(Room room);
        Task DeleteRoomAsync(int id);

        Task<IEnumerable<Building>> GetBuildingsAsync();
        Task<IEnumerable<RoomType>> GetRoomTypesAsync();
        Task SyncAllRoomsOccupancyAsync();
    }
}

