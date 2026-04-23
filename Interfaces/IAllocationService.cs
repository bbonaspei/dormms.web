using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IAllocationService
    {

        Task<bool> CreateAllocationAsync(int studentId, int roomId, DateTime startDate, DateTime endDate, decimal deposit, string keyCard);

        Task<IEnumerable<Allocation>> GetActiveAllocationsAsync();

        Task<bool> ProcessAllocationAsync(Allocation allocation);

        Task<bool> TerminateAllocationAsync(int id);

        Task<bool> RequestAllocationAsync(int studentId, int roomId);

        Task<Allocation?> GetStudentActiveAllocationAsync(int studentId);
    }
}

