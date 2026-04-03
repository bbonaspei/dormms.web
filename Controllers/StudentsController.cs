using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using DormMS.Web.Data; // YENİ: Veritabanı yolu eklendi
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IDocumentService _documentService;
        private readonly ApplicationDbContext _context; // YENİ: Veritabanı bağlantısı tanımlandı

        // CONSTRUCTOR: _context artık buraya dahil edildi (Hata burada çözüldü)
        public StudentsController(IStudentService studentService, IDocumentService documentService, ApplicationDbContext context)
        {
            _studentService = studentService;
            _documentService = documentService;
            _context = context; // Atama yapıldı
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetStudentListAsync();
            return View(students);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int id)
        {
            // _context artık yukarıda tanıtıldığı için hata vermeyecek
            var student = await _context.Students
                .Include(s => s.User)
                .Include(s => s.Room)
                .FirstOrDefaultAsync(s => s.id == id);

            if (student == null) return NotFound();

            // Simülasyon kontrolü
            ViewBag.IsAdmin = (student.User?.username == "admin");

            ViewBag.Documents = await _context.StudentDocuments.Where(d => d.studentId == id).ToListAsync();
            return View(student);
        }

        // GET: Students/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student, string firstName, string lastName, string email)
        {
            ModelState.Remove("User");
            if (ModelState.IsValid)
            {
                await _studentService.EnrollNewStudentAsync(student, firstName, lastName, email);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // POST: Students/UploadDoc
        [HttpPost]
        public async Task<IActionResult> UploadDoc(int studentId, string docType, IFormFile file)
        {
            var result = await _documentService.UploadDocumentAsync(studentId, docType, file);
            return RedirectToAction("Details", new { id = studentId });
        }
    }
}