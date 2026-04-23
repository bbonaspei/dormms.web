using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task AddAsync(Student student);

        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
    }
}

