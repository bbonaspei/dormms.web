using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAvailableRoomsAsync();
        Task<Room> GetRoomDetailsAsync(int id);
        Task CreateRoomAsync(Room room);

        // SONUNDA S TAKISI VAR (Çoğul)
        Task<IEnumerable<Building>> GetBuildingsAsync();
        Task<IEnumerable<RoomType>> GetRoomTypesAsync();
    }
}