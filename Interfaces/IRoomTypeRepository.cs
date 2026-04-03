using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IRoomTypeRepository
    {
        Task<IEnumerable<RoomType>> GetAllAsync();
        Task AddAsync(RoomType roomType);
    }
}