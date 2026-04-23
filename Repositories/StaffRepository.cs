using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ApplicationDbContext _context;

        public StaffRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllStaffAsync()
        {
            return await _context.Users
                .Include(u => u.UserRoles!)
                    .ThenInclude(ur => ur.Role)
                .Where(u => u.UserRoles!.Any(ur => ur.Role != null && ur.Role.RoleName != "Student"))
                .ToListAsync();
        }

        public async Task<User?> GetStaffByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.UserRoles!)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> AddStaffAsync(User user, int roleId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                _context.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = roleId });
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> UpdateStaffAsync(User user)
        {
            try
            {

                var existing = await _context.Users.FindAsync(user.Id);
                if (existing == null) return false;

                existing.firstName = user.firstName;
                existing.lastName = user.lastName;
                existing.email = user.email;
                existing.username = user.username;
                existing.isActive = user.isActive;
                existing.passwordHash = user.passwordHash;
                existing.updatedAt = user.updatedAt;

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteStaffAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null) return false;

                var roles = await _context.UserRoles.Where(ur => ur.UserId == id).ToListAsync();
                _context.UserRoles.RemoveRange(roles);

                var tasks = await _context.MaintenanceRequests.Where(m => m.assignedTo == id).ToListAsync();
                foreach (var task in tasks) task.assignedTo = null;

                var userLogs = await _context.AuditLogs.Where(l => l.userId == id).ToListAsync();
                foreach (var log in userLogs) log.userId = null;

                _context.Users.Remove(user);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}

