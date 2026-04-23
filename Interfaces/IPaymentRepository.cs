using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment?> GetPaymentByIdAsync(int id);
        Task<bool> AddPaymentAsync(Payment payment);
        Task<IEnumerable<Payment>> GetPaymentsByStudentIdAsync(int studentId);
    }
}

