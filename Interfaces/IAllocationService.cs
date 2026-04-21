using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IAllocationService
    {
        // Rapor Diyagram 3'e göre: Detaylı atama süreci
        Task<bool> CreateAllocationAsync(int studentId, int roomId, DateTime startDate, DateTime endDate, decimal deposit, string keyCard);

        // Aktif yerleştirmeleri listeleme
        Task<IEnumerable<Allocation>> GetActiveAllocationsAsync();

        // Formdan gelen nesneyi komple işleme
        Task<bool> ProcessAllocationAsync(Allocation allocation);
        // IAllocationService.cs içine ekle
        Task<bool> TerminateAllocationAsync(int id);
        
        // Student room request
        Task<bool> RequestAllocationAsync(int studentId, int roomId);
        
        // Check if student has active or pending allocation
        Task<Allocation?> GetStudentActiveAllocationAsync(int studentId);
    }
}