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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fees.Include(f => f.RoomTypes).ToListAsync());
        }

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

                await _audit.LogActionAsync("CREATE", "Fee", fee.id, null, $"New fee category: {fee.feeName} created.");

                return RedirectToAction(nameof(Index));
            }
            return View(fee);
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var fee = await _context.Fees
                .Include(f => f.RoomTypes)
                .FirstOrDefaultAsync(f => f.id == id);

            if (fee == null) return NotFound();

            bool isLinkedToRoomType = fee.RoomTypes != null && fee.RoomTypes.Any();

            bool isLinkedToStudent = await _context.StudentFees.AnyAsync(sf => sf.feeId == id);

            if (isLinkedToRoomType || isLinkedToStudent)
            {
                TempData["Error"] = "Cannot delete this fee category because it is actively linked to room classifications or existing student financial records. Please unlink it first.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Fees.Remove(fee);
                await _context.SaveChangesAsync();

                await _audit.LogActionAsync("DELETE", "Fee", id, fee.feeName, "Permanently deleted fee category.");
                TempData["Success"] = "Fee category has been permanently removed.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the fee: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool FeeExists(int id)
        {
            return _context.Fees.Any(e => e.id == id);
        }
    }
}

