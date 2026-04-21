using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DormMS.Web.Data;
using DormMS.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DormMS.Web.Interfaces;

namespace DormMS.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Staff/[action]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class StaffController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuditService _audit;

        public StaffController(ApplicationDbContext context, IAuditService audit)
        {
            _context = context;
            _audit = audit;
        }

        // --- PERSONEL LİSTESİ (Sadece Personelleri Getirir) ---
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Öğrenci (Student) rolüne sahip OLMAYAN tüm kullanıcıları getir
            var staffList = await _context.Users
                .Include(u => u.UserRoles!)
                    .ThenInclude(ur => ur.Role)
                .Where(u => u.UserRoles!.Any(ur => ur.Role != null && ur.Role.RoleName != "Student"))
                .ToListAsync();

            return View(staffList);
        }

        // --- PERSONEL DETAY SAYFASI (YENİ EKLENDİ) ---
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            // 1. Personel bilgilerini getir
            var staff = await _context.Users
                .Include(u => u.UserRoles!)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (staff == null) return NotFound();

            // 2. KRİTİK NOKTA: Bu personelin (id) yaptığı son 5 işlemi veritabanından çek
            ViewBag.UserLogs = await _context.AuditLogs
                .Where(l => l.userId == id) // Sadece bu personelin logları
                .OrderByDescending(l => l.createdAt)
                .Take(5)
                .ToListAsync();

            return View(staff);
        }

        // --- YENİ PERSONEL KAYDI (GET) ---
        [HttpGet]
        public IActionResult Create()
        {
            // Dropdown listesinde sadece personel olabilecek rolleri göster (Öğrenciyi hariç tut)
            ViewBag.Roles = new SelectList(_context.Roles.Where(r => r.RoleName != "Student"), "Id", "RoleName");
            return View();
        }

        // --- YENİ PERSONEL KAYDI (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user, int roleId)
        {
            // Rapor Sayfa 6: Varsayılan başlangıç şifresi ataması (123456)
            user.passwordHash = "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=";
            user.isActive = true;
            user.createdAt = DateTime.Now;
            user.updatedAt = DateTime.Now;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Seçilen rolü ara tabloya kaydet
            _context.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = roleId });
            await _context.SaveChangesAsync();

            // AUDIT LOG: Personel kaydını sisteme mühürle
            await _audit.LogActionAsync("CREATE", "Staff", user.Id, null, $"Staff Member {user.firstName} {user.lastName} successfully enrolled.");

            return RedirectToAction(nameof(Index));
        }
        // --- PERSONEL SİLME (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            // Güvenlik: Admin kendini silemesin (opsiyonel ama iyi uygulama)
            var currentUserId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (id == currentUserId)
            {
                TempData["Error"] = "Safety Lock: You cannot terminate your own administrative account.";
                return RedirectToAction(nameof(Index));
            }

            // AUDIT LOG: Silme işleminden ÖNCE kaydı tut (çünkü user nesnesi gidecek)
            await _audit.LogActionAsync("DELETE", "Staff", id, null, $"Staff Member {user.firstName} {user.lastName} has been permanently removed from the system registry.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Staff record successfully purged from the infrastructure.";
            return RedirectToAction(nameof(Index));
        }
    }
}