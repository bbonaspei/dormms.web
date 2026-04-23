using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IMaintenanceService
    {

        Task<IEnumerable<MaintenanceRequest>> GetActiveRequestsAsync(int? studentId = null);
        Task<IEnumerable<MaintenanceRequest>> GetArchivedRequestsAsync(int? studentId = null);
        Task<MaintenanceRequest?> GetRequestByIdAsync(int id);
        Task CreateRequestAsync(MaintenanceRequest request);
        Task UpdateStatusAsync(int requestId, string newStatus);
        Task DeleteRequestAsync(int id);

        Task<bool> AssignStaffAsync(int requestId, int staffId);
        Task AddFeedbackAsync(int requestId, int rating, string feedback);

        Task<Student?> GetStudentProfileAsync(int userId);
        Task<dynamic> GetCreateDropdownDataAsync(int userId, bool isStudent);
        Task<IEnumerable<User>> GetAvailableStaffAsync();
        Task<Room?> GetStudentRoomAsync(int studentId);
    }
}

