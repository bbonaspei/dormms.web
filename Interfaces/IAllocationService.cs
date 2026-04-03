using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IAllocationService
    {
        // 1. Diyagram 3, Adım 14'teki ID bazlı görev
        Task<bool> CreateAllocationAsync(int studentId, int roomId, DateTime startDate, DateTime endDate);

        // 2. Aktif yerleştirmeleri listeleme görevi
        Task<IEnumerable<Allocation>> GetActiveAllocationsAsync();

        // 3. Formdan gelen tüm nesneyi işleme görevi
        Task<bool> ProcessAllocationAsync(Allocation allocation);
    }
}