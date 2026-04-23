using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IAuditService
    {
        Task LogActionAsync(string action, string entityType, int entityId, string? oldVal = null, string? newVal = null);

        Task<IEnumerable<AuditLog>> GetAllLogsAsync();
    }
}

