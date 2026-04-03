using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly ApplicationDbContext _context;
        public DocumentService(ApplicationDbContext context) { _context = context; }

        public async Task<bool> UploadDocumentAsync(int studentId, string docType, IFormFile file)
        {
            if (file == null || file.Length == 0) return false;

            // 1. Klasör yolunu hazırla (wwwroot/documents)
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/documents");
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            // 2. Benzersiz dosya adı oluştur
            var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(folderPath, fileName);

            // 3. Dosyayı sunucuya kopyala
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 4. Veritabanına kaydet
            var doc = new StudentDocument
            {
                studentId = studentId,
                documentType = docType,
                documentName = file.FileName,
                filePath = "/documents/" + fileName
            };
            _context.StudentDocuments.Add(doc);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<StudentDocument>> GetStudentDocumentsAsync(int studentId)
        {
            return await _context.StudentDocuments
                .Where(d => d.studentId == studentId)
                .ToListAsync();
        }
    }
}