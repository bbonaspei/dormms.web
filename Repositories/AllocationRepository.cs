using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Repositories
{
    public class AllocationRepository : IAllocationRepository
    {
        private readonly ApplicationDbContext _context;
        public AllocationRepository(ApplicationDbContext context) { _context = context; }

        public async Task<IEnumerable<Allocation>> GetAllAsync()
        {
            return await _context.Allocations
                .Include(a => a.Student).ThenInclude(s => s!.User)
                .Include(a => a.Room)
                .ToListAsync();
        }

        public async Task AddAsync(Allocation allocation)
        {
            await _context.Allocations.AddAsync(allocation);
            await _context.SaveChangesAsync();
        }

        public async Task<Allocation?> GetByIdAsync(int id) => await _context.Allocations.FindAsync(id);

        public async Task<Allocation?> GetActiveByStudentIdAsync(int studentId)
        {
            return await _context.Allocations
                .Include(a => a.Room).ThenInclude(r => r.RoomType)
                .FirstOrDefaultAsync(a => a.studentId == studentId && a.isCurrent == true);
        }
    }
}