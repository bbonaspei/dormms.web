using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IDocumentRepository
    {

        Task<IEnumerable<StudentDocument>> GetStudentDocumentsAsync(int studentId);
        Task<bool> AddStudentDocumentAsync(StudentDocument doc);
        Task<bool> DeleteStudentDocumentAsync(int id);

        Task<IEnumerable<Document>> GetAllDocumentsAsync();
        Task<bool> AddDocumentAsync(Document doc);
    }
}

