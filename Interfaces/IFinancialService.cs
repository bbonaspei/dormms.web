using DormMS.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DormMS.Web.Interfaces
{
    public interface IFinancialService
    {
        Task<IEnumerable<Payment>> GetAllTransactionsAsync();
        Task<IEnumerable<StudentFee>> GetOutstandingDuesAsync(int studentId);
        Task<bool> ProcessPaymentAsync(Payment payment, int feeId);
    }
}