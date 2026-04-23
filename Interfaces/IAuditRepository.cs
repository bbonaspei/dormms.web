using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IAuditRepository
    {
        Task AddAsync(AuditLog log);
        Task<IEnumerable<AuditLog>> GetAllAsync();
    }
}

