using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudentListAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        // Yeni öğrenci kaydı sırasında kullanıcı bilgilerini de alıyoruz
        Task EnrollNewStudentAsync(Student student, string firstName, string lastName, string email);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
        Task<Student?> GetStudentByUserIdAsync(int userId);
    }
}