using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IDocumentRepository
    {
        // Student Documents
        Task<IEnumerable<StudentDocument>> GetStudentDocumentsAsync(int studentId);
        Task<bool> AddStudentDocumentAsync(StudentDocument doc);
        Task<bool> DeleteStudentDocumentAsync(int id);
        
        // General Documents (from Task D)
        Task<IEnumerable<Document>> GetAllDocumentsAsync();
        Task<bool> AddDocumentAsync(Document doc);
    }
}
