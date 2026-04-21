using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IMaintenanceRepository
    {
        Task<IEnumerable<MaintenanceRequest>> GetAllAsync();
        Task<MaintenanceRequest?> GetByIdAsync(int id);
        Task AddAsync(MaintenanceRequest request);
        Task UpdateAsync(MaintenanceRequest request);
        Task DeleteAsync(int id);

        // Ekstra Data İhtiyaçları
        Task<Student?> GetStudentByUserIdAsync(int userId);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<Room?> GetRoomByStudentIdAsync(int studentId);
        Task<IEnumerable<UserRole>> GetStaffUsersAsync();
    }
}