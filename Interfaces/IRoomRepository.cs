using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<Room> GetByIdAsync(int id);   
        
        Task UpdateAsync(Room room);

        Task AddAsync(Room room);   
    }
}
