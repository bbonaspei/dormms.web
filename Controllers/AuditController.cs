using Microsoft.AspNetCore.Mvc;
using DormMS.Web.Interfaces;

namespace DormMS.Web.Controllers
{
    public class AuditController : Controller
    {
        private readonly IAuditService _auditService;

        public AuditController(IAuditService auditService)
        {
            _auditService = auditService;
        }

        // GET: Audit (Log Listesi Sayfası)
        public async Task<IActionResult> Index()
        {
            // Servis üzerinden tüm kayıtları çekip sayfaya gönderiyoruz
            var logs = await _auditService.GetAllLogsAsync();
            return View(logs);
        }
    }
}