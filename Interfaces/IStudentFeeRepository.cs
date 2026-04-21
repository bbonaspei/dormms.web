using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IStudentFeeRepository
    {
        Task<IEnumerable<StudentFee>> GetAllAsync();
        Task<IEnumerable<StudentFee>> GetByStudentIdAsync(int studentId);
        Task<StudentFee?> GetByIdAsync(int id);
        Task<bool> AddAsync(StudentFee fee);
        Task<bool> UpdateAsync(StudentFee fee);
        Task<bool> SaveChangesAsync();
    }
}
