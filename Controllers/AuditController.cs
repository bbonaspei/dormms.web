using DormMS.Web.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormMS.Web.Controllers
{
    [Authorize(Roles = "Admin")]

    [Route("Audit/[action]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class AuditController : Controller
    {
        private readonly IAuditService _auditService;

        public AuditController(IAuditService auditService)
        {
            _auditService = auditService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var logs = await _auditService.GetAllLogsAsync();
            return View(logs);
        }
    }
}