using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DormMS.Web.Services
{
    public class FinancialService : IFinancialService
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationService _notificationService; // Bunu ekledik

        // Constructor güncellendi
        public FinancialService(ApplicationDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<IEnumerable<Payment>> GetAllTransactionsAsync()
        {
            return await _context.Payments
                .Include(p => p.Student)
                    .ThenInclude(s => s.User)
                .OrderByDescending(p => p.paymentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentFee>> GetOutstandingDuesAsync(int studentId)
        {
            return await _context.StudentFees
                .Include(f => f.Fee)
                .Where(f => f.studentId == studentId && f.status == "Unpaid")
                .ToListAsync();
        }

        public async Task<bool> ProcessPaymentAsync(Payment payment, int feeId)
        {
            payment.paymentReference = "RCPT-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            _context.Payments.Add(payment);

            var fee = await _context.StudentFees.FindAsync(feeId);
            if (fee != null)
            {
                fee.status = "Paid";
                _context.StudentFees.Update(fee);
            }

            // BİLDİRİM GÖNDERME MANTIĞI (Diyagram 2 - Adım 24)
            // 1 numaralı kullanıcıya (Admin) ödeme bildirimi gönderir
            await _notificationService.SendNotificationAsync(1, "Payment Received", $"{payment.amount}$ has been collected.", "Success");

            await _context.SaveChangesAsync();
            return true;
        }
    }
}