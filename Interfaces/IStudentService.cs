using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IStudentService 
    {
        Task<IEnumerable<Student>> GetStudentListAsync();
        Task EnrollStudentAsync(Student student, User user);
        Task EnrollNewStudentAsync(Student student, string firstName, string lastName, string email);
        // HATA VEREN EKSİK SATIR BURASIYDI, EKLEDİK:
        Task<Student?> GetStudentByIdAsync(int id);
    }
}
