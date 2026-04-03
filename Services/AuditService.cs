using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Services
{
    public class AuditService : IAuditService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public AuditService(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public async Task LogActionAsync(string action, string entityType, int entityId, string? oldVal = null, string? newVal = null)
        {
            var log = new AuditLog
            {
                action = action,
                entityType = entityType,
                entityId = entityId,
                oldValues = oldVal,
                newValues = newVal,
                ipAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                userAgent = _httpContext.HttpContext?.Request?.Headers["User-Agent"].ToString(),
                createdAt = DateTime.Now
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        // İsmi GetAllLogsAsync (Çoğul) olarak güncelledik
        public async Task<IEnumerable<AuditLog>> GetAllLogsAsync()
        {
            return await _context.AuditLogs
                .Include(a => a.User)
                .OrderByDescending(a => a.createdAt)
                .ToListAsync();
        }
    }
}