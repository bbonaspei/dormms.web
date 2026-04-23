using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IStaffRepository
    {
        Task<IEnumerable<User>> GetAllStaffAsync();
        Task<User?> GetStaffByIdAsync(int id);
        Task<bool> AddStaffAsync(User user, int roleId);
        Task<bool> UpdateStaffAsync(User user);
        Task<bool> DeleteStaffAsync(int id);
    }
}

