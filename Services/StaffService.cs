using DormMS.Web.Interfaces;
using DormMS.Web.Models;

namespace DormMS.Web.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepo;
        private readonly IAuditService _audit;

        public StaffService(IStaffRepository staffRepo, IAuditService audit)
        {
            _staffRepo = staffRepo;
            _audit = audit;
        }

        public async Task<IEnumerable<User>> GetStaffListAsync()
        {
            return await _staffRepo.GetAllStaffAsync();
        }

        public async Task<User?> GetStaffByIdAsync(int id)
        {
            return await _staffRepo.GetStaffByIdAsync(id);
        }

        public async Task<bool> AddStaffAsync(User user, int roleId)
        {

            if (string.IsNullOrEmpty(user.passwordHash))
            {
                user.passwordHash = "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=";
            }
            
            user.isActive = true;
            user.createdAt = DateTime.Now;
            user.updatedAt = DateTime.Now;

            bool success = await _staffRepo.AddStaffAsync(user, roleId);
            if (success)
            {
                await _audit.LogActionAsync("CREATE", "Staff", user.Id, null, $"Staff Member {user.firstName} {user.lastName} successfully enrolled.");
            }
            return success;
        }

        public async Task<bool> UpdateStaffAsync(User user)
        {
            user.updatedAt = DateTime.Now;
            bool success = await _staffRepo.UpdateStaffAsync(user);
            if (success)
            {
                await _audit.LogActionAsync("UPDATE", "Staff", user.Id, null, $"Information updated for staff member {user.firstName} {user.lastName}.");
            }
            return success;
        }

        public async Task<bool> DeleteStaffAsync(int id)
        {
            var user = await _staffRepo.GetStaffByIdAsync(id);
            if (user == null) return false;

            await _audit.LogActionAsync("DELETE", "Staff", id, null, $"Initiating deletion of staff member {user.firstName} {user.lastName}");
            
            return await _staffRepo.DeleteStaffAsync(id);
        }
    }
}

