using DormMS.Web.Interfaces;
using DormMS.Web.Models;

namespace DormMS.Web.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepo;
        public DocumentService(IDocumentRepository documentRepo) { _documentRepo = documentRepo; }

        public async Task<bool> UploadDocumentAsync(int studentId, string docType, IFormFile file)
        {
            try
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/documents");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{studentId}_{Guid.NewGuid().ToString().Substring(0, 4)}_{file.FileName}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var doc = new StudentDocument
                {
                    studentId = studentId,
                    documentType = docType,
                    documentName = file.FileName,
                    filePath = "/uploads/documents/" + fileName,
                    fileSize = (int)file.Length,
                    uploadedAt = DateTime.Now
                };

                return await _documentRepo.AddStudentDocumentAsync(doc);
            }
            catch { return false; }
        }

        public async Task<IEnumerable<StudentDocument>> GetStudentDocumentsAsync(int studentId)
        {
            return await _documentRepo.GetStudentDocumentsAsync(studentId);
        }
    }
}

