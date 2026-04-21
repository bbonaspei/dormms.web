using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface INotificationService
    {
        // 5. Parametre olan targetUrl eklendi
        Task SendNotificationAsync(int userId, string title, string message, string type, string targetUrl = "#");
        Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId);
        Task MarkAsReadAsync(int notificationId);
    }
}