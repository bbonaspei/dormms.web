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

        public async Task SendNotificationAsync(int userId, string title, string message, string type, string targetUrl = "#")
        {
            var notification = new Notification
            {
                userId = userId,
                title = title,
                message = message,
                type = type,
                targetUrl = targetUrl,
                createdAt = DateTime.Now,
                isRead = false
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId)
        {
            return await _context.Notifications
                .Where(n => n.userId == userId && !n.isRead)
                .OrderByDescending(n => n.createdAt)
                .Take(5)
                .ToListAsync();
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var note = await _context.Notifications.FindAsync(notificationId);
            if (note != null) { note.isRead = true; await _context.SaveChangesAsync(); }
        }

        public async Task SendNotificationToStudentAsync(int studentId, string title, string message, string type, string targetUrl = "#")
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student != null)
            {
                await SendNotificationAsync(student.userId, title, message, type, targetUrl);
            }
        }
    }
}

