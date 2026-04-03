using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;
        public NotificationService(ApplicationDbContext context) { _context = context; }

        public async Task SendNotificationAsync(int userId, string title, string message, string type = "Info")
        {
            var notification = new Notification
            {
                userId = userId,
                title = title,
                message = message,
                type = type,
                createdAt = DateTime.Now
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId)
        {
            return await _context.Notifications
                .Where(n => n.userId == userId)
                .OrderByDescending(n => n.createdAt)
                .Take(5) // Sadece son 5 bildirimi gösterelim
                .ToListAsync();
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var note = await _context.Notifications.FindAsync(notificationId);
            if (note != null)
            {
                note.isRead = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}