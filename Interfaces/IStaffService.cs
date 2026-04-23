using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IStaffService
    {
        Task<IEnumerable<User>> GetStaffListAsync();
        Task<User?> GetStaffByIdAsync(int id);
        Task<bool> AddStaffAsync(User user, int roleId);
        Task<bool> UpdateStaffAsync(User user);
        Task<bool> DeleteStaffAsync(int id);
    }
}

