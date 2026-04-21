using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Controllers
{
    [Authorize(Roles = "Admin,Finance")]
    [Route("Fees/[action]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class FeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuditService _audit;

        public FeesController(ApplicationDbContext context, IAuditService audit)
        {
            _context = context;
            _audit = audit;
        }

        // --- LİSTELEME ---
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fees.ToListAsync());
        }

        // --- YENİ ÜCRET TANIMLAMA ---
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Fee fee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fee);
                await _context.SaveChangesAsync();

                // AUDIT LOG: Yeni ücret tipi tanımlama (Rapor Sayfa 22)
                await _audit.LogActionAsync("CREATE", "Fee", fee.id, null, $"New fee category: {fee.feeName} created.");

                return RedirectToAction(nameof(Index));
            }
            return View(fee);
        }

        // --- DÜZENLEME İŞLEMLERİ (YENİ EKLENDİ - BUTONU ÇALIŞTIRIR) ---

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var fee = await _context.Fees.FindAsync(id);
            if (fee == null) return NotFound();

            return View(fee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Fee fee)
        {
            if (id != fee.id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fee);
                    await _context.SaveChangesAsync();

                    // AUDIT LOG: Ücret güncelleme kaydı
                    await _audit.LogActionAsync("UPDATE", "Fee", fee.id, null, $"Fee {fee.feeName} configuration updated.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeeExists(fee.id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fee);
        }

        private bool FeeExists(int id)
        {
            return _context.Fees.Any(e => e.id == id);
        }
    }
}