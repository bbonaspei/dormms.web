using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Repositories
{
    public class MaintenanceRepository : IMaintenanceRepository
    {
        private readonly ApplicationDbContext _context;

        public MaintenanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MaintenanceRequest>> GetAllAsync()
        {
            return await _context.MaintenanceRequests
                .Include(r => r.Student).ThenInclude(s => s!.User)
                .Include(r => r.Room)
                .Include(r => r.Staff)
                .OrderByDescending(r => r.requestDate)
                .ToListAsync();
        }

        public async Task<MaintenanceRequest?> GetByIdAsync(int id)
        {
            return await _context.MaintenanceRequests
                .Include(r => r.Student).ThenInclude(s => s!.User)
                .Include(r => r.Room)
                .Include(r => r.Staff)
                .FirstOrDefaultAsync(r => r.id == id);
        }

        public async Task AddAsync(MaintenanceRequest request)
        {
            await _context.MaintenanceRequests.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MaintenanceRequest request)
        {
            _context.MaintenanceRequests.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var request = await _context.MaintenanceRequests.FindAsync(id);
            if (request != null)
            {
                _context.MaintenanceRequests.Remove(request);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Student?> GetStudentByUserIdAsync(int userId) =>
            await _context.Students.Include(s => s.User).FirstOrDefaultAsync(s => s.userId == userId);

        public async Task<IEnumerable<Student>> GetAllStudentsAsync() =>
            await _context.Students.Include(s => s.User).ToListAsync();

        public async Task<IEnumerable<Room>> GetAllRoomsAsync() =>
            await _context.Rooms.ToListAsync();

        public async Task<Room?> GetRoomByStudentIdAsync(int studentId)
        {
            // Öğrencinin aktif kaldığı odayı bulur
            var allocation = await _context.Allocations
                .Include(a => a.Room)
                .FirstOrDefaultAsync(a => a.studentId == studentId && a.isCurrent == true);
            return allocation?.Room;
        }

        public async Task<IEnumerable<UserRole>> GetStaffUsersAsync() =>
            await _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .Where(ur => ur.Role != null && ur.Role.RoleName != "Student")
                .ToListAsync();
    }
}