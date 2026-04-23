using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using System.Security.Claims;

namespace DormMS.Web.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepo;
        private readonly IHttpContextAccessor _httpContext;

        public AuditService(IAuditRepository auditRepo, IHttpContextAccessor httpContext)
        {
            _auditRepo = auditRepo;
            _httpContext = httpContext;
        }

        public async Task LogActionAsync(string action, string entityType, int entityId, string? oldVal = null, string? newVal = null)
        {
            var userIdString = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int? currentUserId = string.IsNullOrEmpty(userIdString) ? null : int.Parse(userIdString);

            var log = new AuditLog
            {
                userId = currentUserId,
                action = action,
                entityType = entityType,
                entityId = entityId,
                oldValues = oldVal,
                newValues = newVal,
                ipAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                userAgent = _httpContext.HttpContext?.Request?.Headers["User-Agent"].ToString(),
                createdAt = DateTime.Now
            };

            await _auditRepo.AddAsync(log);
        }

        public async Task<IEnumerable<AuditLog>> GetAllLogsAsync()
        {
            return await _auditRepo.GetAllAsync();
        }
    }
}

