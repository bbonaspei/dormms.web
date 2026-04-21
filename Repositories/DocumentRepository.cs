using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDbContext _context;

        public DocumentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentDocument>> GetStudentDocumentsAsync(int studentId) 
            => await _context.StudentDocuments.Where(d => d.studentId == studentId).ToListAsync();

        public async Task<bool> AddStudentDocumentAsync(StudentDocument doc)
        {
            _context.StudentDocuments.Add(doc);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteStudentDocumentAsync(int id)
        {
            var doc = await _context.StudentDocuments.FindAsync(id);
            if (doc == null) return false;
            _context.StudentDocuments.Remove(doc);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Document>> GetAllDocumentsAsync() => await _context.Documents.Include(d => d.Uploader).ToListAsync();

        public async Task<bool> AddDocumentAsync(Document doc)
        {
            _context.Documents.Add(doc);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
