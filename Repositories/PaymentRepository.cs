using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync() => await _context.Payments.Include(p => p.Student).ThenInclude(s => s.User).ToListAsync();

        public async Task<Payment?> GetPaymentByIdAsync(int id) => await _context.Payments.FindAsync(id);

        public async Task<bool> AddPaymentAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByStudentIdAsync(int studentId) 
            => await _context.Payments.Where(p => p.studentId == studentId).ToListAsync();
    }
}

