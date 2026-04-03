using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IMaintenanceService 
    {
        Task<IEnumerable<MaintenanceRequest>> GetAllRequestsAsync();
        Task CreateRequestAsync(MaintenanceRequest request);
        Task UpdateStatusAsync(int requestId, string newStatus);

    }
}
