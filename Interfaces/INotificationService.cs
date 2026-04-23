using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface INotificationService
    {

        Task SendNotificationAsync(int userId, string title, string message, string type, string targetUrl = "#");
        Task SendNotificationToStudentAsync(int studentId, string title, string message, string type, string targetUrl = "#");
        Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId);
        Task MarkAsReadAsync(int notificationId);
    }
}

