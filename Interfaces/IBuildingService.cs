using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IBuildingService 
    {
        Task<IEnumerable<Building>> GetBuildingsListAsync();
        Task<Building?> GetBuildingByIdAsync(int id);
        Task UpdateBuildingAsync(Building building);
        Task CreateBuildingAsync(Building building);
        Task DeleteBuildingAsync(int id);
    }
}
