using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DormMS.Web.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Claims;

namespace DormMS.Web.Controllers
{
    [Authorize]
    [Route("Payments/[action]/{id?}")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class PaymentsController : Controller
    {
        private readonly IFinancialService _financialService;
        private readonly ApplicationDbContext _context;

        public PaymentsController(IFinancialService financialService, ApplicationDbContext context)
        {
            _financialService = financialService;
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager,DormManager,Student,Finance")]
        public async Task<IActionResult> Index(int? studentId)
        {
            // 1. Giriş yapan kullanıcının ID'sini al
            var userIdStr = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

            var userId = int.Parse(userIdStr);

            // 2. Tüm finansal hareketleri çek
            var payments = await _financialService.GetAllTransactionsAsync();

            // --- ROL BAZLI FİLTRELEME ---
            if (User.IsInRole("Student"))
            {
                // Öğrenci sadece kendi ödemelerini görsün
                var student = await _context.Students.FirstOrDefaultAsync(s => s.userId == userId);
                if (student != null)
                {
                    // OTOMATİK BORÇLANDIRMA SİNKRONİZASYONU (SÖZLEŞMEYE GÖRE)
                    await _financialService.SyncStudentChargesAsync(student.id);

                    payments = payments.Where(p => p.studentId == student.id);

                    // EKLEME: Öğrenci için bekleyen ve geçmiş borçları/ücretleri de çekiyoruz (FİNANS EKRANI İÇİN)
                    var allFees = await _context.StudentFees
                        .Include(f => f.Fee)
                        .Where(f => f.studentId == student.id)
                        .ToListAsync();

                    // CEZALARI ÇEK (YENİ EKLENDİ)
                    var penalties = await _context.Penalties
                        .Where(p => p.studentId == student.id && p.status == "Pending")
                        .ToListAsync();

                    ViewBag.PendingFees = allFees.Where(f => f.status == "Unpaid").OrderBy(f => f.dueDate).ToList();
                    ViewBag.PaidFees = allFees.Where(f => f.status == "Paid").OrderByDescending(f => f.dueDate).ToList();
                    ViewBag.Penalties = penalties;
                    ViewBag.TotalBalance = allFees.Where(f => f.status == "Unpaid").Sum(f => f.amount) + penalties.Sum(p => p.amount);
                    ViewBag.IsStudent = true;
                    ViewBag.StudentIdForPayment = student.id;
                }
            }
            else if (studentId.HasValue)
            {
                // OTOMATİK BORÇLANDIRMA SİNKRONİZASYONU (SÖZLEŞMEYE GÖRE)
                await _financialService.SyncStudentChargesAsync(studentId.Value);

                // Admin filtrelemesi
                payments = payments.Where(p => p.studentId == studentId.Value);
                
                var penalties = await _context.Penalties
                        .Where(p => p.studentId == studentId.Value && p.status == "Pending")
                        .ToListAsync();

                ViewBag.Penalties = penalties;
                ViewBag.FilteredStudent = studentId.Value;
                ViewBag.IsStudent = false;
            }
            else
            {
                ViewBag.IsStudent = false;
            }

            return View(payments.ToList());
        }

        // YENİ: Raporu Dışa Aktarma Metodu (Rapor Sayfa 5 Uyumlu)
        [HttpGet]
        [Authorize(Roles = "Admin,Manager,DormManager,Finance")]
        public async Task<IActionResult> ExportReport()
        {
            var payments = await _financialService.GetAllTransactionsAsync();

            var sb = new StringBuilder();
            sb.AppendLine("DORMMS - INSTITUTIONAL FINANCIAL AUDIT REPORT");
            sb.AppendLine($"Generated on: {DateTime.Now:dd/MM/yyyy HH:mm}");
            sb.AppendLine("----------------------------------------------------------------------");
            sb.AppendLine(string.Format("{0,-15} | {1,-20} | {2,-15} | {3,-10}", "REFERENCE", "STUDENT", "DATE", "AMOUNT"));
            sb.AppendLine("----------------------------------------------------------------------");

            foreach (var item in payments)
            {
                string studentName = $"{item.Student?.User?.firstName} {item.Student?.User?.lastName}";
                sb.AppendLine(string.Format("{0,-15} | {1,-20} | {2,-15} | ${3,-10}",
                    item.paymentReference,
                    studentName,
                    item.paymentDate.ToString("dd/MM/yyyy"),
                    item.amount.ToString("N2")));
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/plain", $"Financial_Audit_{DateTime.Now:yyyyMMdd}.txt");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager,DormManager,Finance")]
        public async Task<IActionResult> Create()
        {
            ViewBag.studentId = new SelectList(await _context.Students.Include(s => s.User).ToListAsync(), "id", "studentId");
            ViewBag.feeId = new SelectList(await _context.Fees.ToListAsync(), "id", "feeName");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager,DormManager,Student,Finance")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Payment payment, int feeId)
        {
            // Güvenlik: Eğer öğrenciyse, sadece kendi adına ödeme yapabilir.
            if (User.IsInRole("Student"))
            {
                var userIdStr = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

                var userId = int.Parse(userIdStr);
                var student = await _context.Students.FirstOrDefaultAsync(s => s.userId == userId);
                if (student == null || payment.studentId != student.id)
                {
                    return Forbid();
                }
            }

            await _financialService.ProcessPaymentAsync(payment, feeId);
            TempData["Success"] = "Payment collected and receipt generated.";
            return RedirectToAction(nameof(Index));
        }
    }
}