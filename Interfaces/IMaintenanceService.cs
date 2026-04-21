using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IMaintenanceService
    {
        // CRUD ve Listeleme
        Task<IEnumerable<MaintenanceRequest>> GetActiveRequestsAsync(int? studentId = null); // Pending, In Progress
        Task<IEnumerable<MaintenanceRequest>> GetArchivedRequestsAsync(int? studentId = null); // Completed, Rejected
        Task<MaintenanceRequest?> GetRequestByIdAsync(int id);
        Task CreateRequestAsync(MaintenanceRequest request);
        Task UpdateStatusAsync(int requestId, string newStatus);
        Task DeleteRequestAsync(int id); // Sadece Admin için

        // İşlemler
        Task AssignStaffAsync(int requestId, int staffId);
        Task AddFeedbackAsync(int requestId, int rating, string feedback);

        // Yardımcı Metotlar (Controller'ı DbContext'ten kurtarmak için)
        Task<Student?> GetStudentProfileAsync(int userId);
        Task<dynamic> GetCreateDropdownDataAsync(int userId, bool isStudent);
        Task<IEnumerable<User>> GetAvailableStaffAsync();
    }
}