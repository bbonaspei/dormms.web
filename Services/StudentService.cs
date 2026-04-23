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
        private readonly IAuditService _audit;
        private readonly INotificationService _notificationService;

        public StudentService(IStudentRepository repo, ApplicationDbContext context, IAuditService audit, INotificationService notificationService)
        {
            _repo = repo;
            _context = context;
            _audit = audit;
            _notificationService = notificationService;
        }

        public async Task<IEnumerable<Student>> GetStudentListAsync()
        {
            return await _context.Students.Include(s => s.User).Include(s => s.Room).ToListAsync();
        }

        public async Task EnrollNewStudentAsync(Student student, string firstName, string lastName, string email)
        {
            var user = new User
            {
                username = student.studentId,
                email = email,
                firstName = firstName,
                lastName = lastName,
                passwordHash = "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=",
                isActive = true
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            student.userId = user.Id;
            student.status = "Active";
            await _repo.AddAsync(student);

            await _audit.LogActionAsync("CREATE", "Student", student.id, null, $"{firstName} {lastName} Registered");

            await _notificationService.SendNotificationAsync(user.Id, "Welcome to DormMS!", "Your account is active. Please change your default password.", "Success", "/Home/Index");
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _context.Students.Include(s => s.User).Include(s => s.Room).FirstOrDefaultAsync(s => s.id == id);
        }

        public async Task UpdateStudentAsync(Student student)
        {
            await _repo.UpdateAsync(student);
            await _audit.LogActionAsync("UPDATE", "Student", student.id, null, "Profile Updated");
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _repo.DeleteAsync(id);
            await _audit.LogActionAsync("DELETE", "Student", id, "Active", "Archived/Deleted");
        }

        public async Task<Student?> GetStudentByUserIdAsync(int userId)
        {
            return await _context.Students
                .Include(s => s.User)
                .Include(s => s.Room)
                .FirstOrDefaultAsync(s => s.userId == userId);
        }
    }
}

