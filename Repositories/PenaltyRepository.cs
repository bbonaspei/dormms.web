using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Repositories
{
    public class PenaltyRepository : IPenaltyRepository
    {
        private readonly ApplicationDbContext _context;

        public PenaltyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Penalty>> GetAllPenaltiesAsync() 
            => await _context.Penalties
                .Include(p => p.Student)
                    .ThenInclude(s => s.User)
                .ToListAsync();

        public async Task<Penalty?> GetPenaltyByIdAsync(int id) => await _context.Penalties.FindAsync(id);

        public async Task<bool> AddPenaltyAsync(Penalty penalty)
        {
            _context.Penalties.Add(penalty);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePenaltyAsync(Penalty penalty)
        {
            _context.Penalties.Update(penalty);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Penalty>> GetByStudentIdAsync(int studentId)
            => await _context.Penalties.Where(p => p.studentId == studentId).ToListAsync();
    }
}

