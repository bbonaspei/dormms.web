using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using DormMS.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;
        private readonly ApplicationDbContext _context;

        public StudentService(IStudentRepository repo, ApplicationDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetStudentListAsync() => await _repo.GetAllAsync();

        public async Task EnrollStudentAsync(Student student, User user)
        {
            // Önce User (Kullanıcı) kaydını yapıyoruz
            user.passwordHash = "123456"; // Varsayılan şifre
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Oluşan User Id'yi Öğrenciye bağlıyoruz
            student.userId = user.id;
            student.status = "Active";

            await _repo.AddAsync(student);
        }

        public async Task EnrollNewStudentAsync(Student student, string firstName, string lastName, string email)
        {
            // 1. Önce bu öğrenci için kurumsal bir 'User' hesabı açıyoruz
            var user = new User
            {
                username = student.studentId, // Okul numarası artık kullanıcı adı
                email = email,
                firstName = firstName,
                lastName = lastName,
                passwordHash = "123456", // Varsayılan şifre
                isActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // 2. Oluşan UserId'yi öğrenciye bağlayıp tüm detaylarıyla kaydediyoruz
            student.userId = user.id;
            student.status = "Active";

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        // HATA VEREN KODUN GERÇEK UYGULAMASI:
        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _context.Students
                .Include(s => s.User) // Öğrencinin isim bilgilerini de (User tablosu) beraber getir
                .FirstOrDefaultAsync(s => s.id == id);
        }
    }
}