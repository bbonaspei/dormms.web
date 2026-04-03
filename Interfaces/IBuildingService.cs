using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IBuildingService 
    {
        Task<IEnumerable<Building>> GetBuildingsListAsync();
        Task CreateBuildingAsync(Building building);
    }
}
