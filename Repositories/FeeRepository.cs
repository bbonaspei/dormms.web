using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Repositories
{
    public class FeeRepository : IFeeRepository
    {
        private readonly ApplicationDbContext _context;

        public FeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Fee>> GetAllFeesAsync() => await _context.Fees.ToListAsync();

        public async Task<Fee?> GetFeeByIdAsync(int id) => await _context.Fees.FindAsync(id);

        public async Task<bool> AddFeeAsync(Fee fee)
        {
            _context.Fees.Add(fee);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateFeeAsync(Fee fee)
        {
            _context.Fees.Update(fee);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteFeeAsync(int id)
        {
            var fee = await _context.Fees.FindAsync(id);
            if (fee == null) return false;
            _context.Fees.Remove(fee);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

