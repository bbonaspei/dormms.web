using DormMS.Web.Interfaces;
using DormMS.Web.Models;

namespace DormMS.Web.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IMaintenanceRepository _repository;
        private readonly INotificationService _notificationService;

        public MaintenanceService(IMaintenanceRepository repository, INotificationService notificationService)
        {
            _repository = repository;
            _notificationService = notificationService;
        }

        public async Task<IEnumerable<MaintenanceRequest>> GetActiveRequestsAsync(int? studentId = null)
        {
            var all = await _repository.GetAllAsync();
            var active = all.Where(r => r.status == "Pending" || r.status == "In Progress");
            return studentId.HasValue 
                ? active.Where(r => r.studentId == studentId.Value || (r.studentId == null && r.roomId == null)) 
                : active;
        }

        public async Task<IEnumerable<MaintenanceRequest>> GetArchivedRequestsAsync(int? studentId = null)
        {
            var all = await _repository.GetAllAsync();
            var archived = all.Where(r => r.status == "Completed" || r.status == "Rejected");
            return studentId.HasValue 
                ? archived.Where(r => r.studentId == studentId.Value || (r.studentId == null && r.roomId == null)) 
                : archived;
        }

        public async Task<MaintenanceRequest?> GetRequestByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task CreateRequestAsync(MaintenanceRequest request)
        {
            request.requestNumber = "REQ-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            request.requestDate = DateTime.Now;

            // KESİN ÇÖZÜM BURASI: Formdan yanlışlıkla ID gelse bile burada eziyoruz.
            // Yeni açılan iş kesinlikle sahipsiz ve "Pending" olmak ZORUNDADIR.
            request.status = "Pending";
            request.assignedTo = null;
            request.completedDate = null;

            // 0 Gelen değerleri null'a çekerek "General / Admin" raporu olmasını sağla
            if (request.studentId == 0) request.studentId = null;
            if (request.roomId == 0) request.roomId = null;

            await _repository.AddAsync(request);

            // RAPOR UYUMU (Sequence Diagram 1, Adım 11): Yöneticiye bildirim gönder
            await _notificationService.SendNotificationAsync(1, "New Maintenance Request", $"Request {request.requestNumber} needs attention.", "Alert", "/Maintenance/Index");
        }

        public async Task UpdateStatusAsync(int requestId, string newStatus)
        {
            var request = await _repository.GetByIdAsync(requestId);
            if (request != null)
            {
                request.status = newStatus;
                if (newStatus == "Completed") request.completedDate = DateTime.Now;
                await _repository.UpdateAsync(request);

                if (request.studentId.HasValue)
                {
                    await _notificationService.SendNotificationAsync(
                        request.studentId.Value, "Maintenance Update", $"Status is now: {newStatus}", "Info", "/Maintenance/Index");
                }
            }
        }

        public async Task DeleteRequestAsync(int id) => await _repository.DeleteAsync(id);

        public async Task AssignStaffAsync(int requestId, int staffId)
        {
            var request = await _repository.GetByIdAsync(requestId);
            if (request != null)
            {
                request.assignedTo = staffId;
                request.status = "In Progress"; // Admin birini atadığı an "In Progress" olur!
                await _repository.UpdateAsync(request);

                if (request.studentId.HasValue)
                {
                    await _notificationService.SendNotificationAsync(
                        request.studentId.Value, "Staff Assigned", "A technician is handling your request.", "Info", "/Maintenance/Index");
                }
            }
        }

        public async Task AddFeedbackAsync(int requestId, int rating, string feedback)
        {
            var request = await _repository.GetByIdAsync(requestId);
            if (request != null)
            {
                request.studentRating = rating;
                request.studentFeedback = feedback;
                await _repository.UpdateAsync(request);
            }
        }

        public async Task<Student?> GetStudentProfileAsync(int userId) => await _repository.GetStudentByUserIdAsync(userId);

        public async Task<dynamic> GetCreateDropdownDataAsync(int userId, bool isStudent)
        {
            if (isStudent)
            {
                var student = await _repository.GetStudentByUserIdAsync(userId);
                var room = student != null ? await _repository.GetRoomByStudentIdAsync(student.id) : null;
                return new { IsStudent = true, Student = student, Room = room };
            }
            return new { IsStudent = false, Students = await _repository.GetAllStudentsAsync(), Rooms = await _repository.GetAllRoomsAsync() };
        }

        public async Task<IEnumerable<User>> GetAvailableStaffAsync()
        {
            var staffRoles = await _repository.GetStaffUsersAsync();
            return staffRoles.Select(ur => ur.User).Distinct().ToList();
        }
    }
}