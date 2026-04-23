using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IDocumentService
    {
        Task<bool> UploadDocumentAsync(int studentId, string docType, IFormFile file);
        Task<IEnumerable<StudentDocument>> GetStudentDocumentsAsync(int studentId);
    }
}

