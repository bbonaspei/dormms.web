using DormMS.Web.Data; // Bunu ekledik
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IFinancialService _financialService;
        private readonly ApplicationDbContext _context; // Bunu ekledik

        // Constructor'a context'i dahil ettik
        public PaymentsController(IFinancialService financialService, ApplicationDbContext context)
        {
            _financialService = financialService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var payments = await _financialService.GetAllTransactionsAsync();
            return View(payments);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? studentId)
        {
            // _context artık burada çalışacak
            ViewBag.studentId = new SelectList(await _context.Students.Include(s => s.User).ToListAsync(), "id", "studentId");
            ViewBag.feeId = new SelectList(await _context.Fees.ToListAsync(), "id", "feeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Payment payment, int feeId)
        {
            // Senin yazdığın mantık aynen duruyor
            payment.paymentReference = "PAY-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            payment.paymentDate = DateTime.Now;
            _context.Payments.Add(payment);

            var studentFee = new StudentFee
            {
                studentId = payment.studentId,
                feeId = feeId,
                amount = payment.amount,
                status = "Paid"
            };
            _context.StudentFees.Add(studentFee);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}