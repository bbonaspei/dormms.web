using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IAllocationRepository 
    {
        Task<IEnumerable<Allocation>> GetAllAsync();
        Task AddAsync(Allocation allocation);
        Task<Allocation?> GetByIdAsync(int id);
        Task<Allocation?> GetActiveByStudentIdAsync(int studentId);
    }
} 
