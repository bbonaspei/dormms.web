using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudentListAsync();
        Task<Student?> GetStudentByIdAsync(int id);

        Task EnrollNewStudentAsync(Student student, string firstName, string lastName, string email);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
        Task<Student?> GetStudentByUserIdAsync(int userId);
    }
}

