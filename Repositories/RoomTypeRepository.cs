using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Repositories
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public RoomTypeRepository(ApplicationDbContext context) { _context = context; }

        public async Task<IEnumerable<RoomType>> GetAllAsync() => await _context.RoomTypes.Include(t => t.Fee).ToListAsync();
        public async Task<RoomType?> GetByIdAsync(int id) => await _context.RoomTypes.Include(t => t.Fee).FirstOrDefaultAsync(t => t.id == id);
        public async Task AddAsync(RoomType roomType) { await _context.RoomTypes.AddAsync(roomType); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(RoomType roomType) { _context.RoomTypes.Update(roomType); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id) { 
            var item = await _context.RoomTypes.FindAsync(id);
            if (item != null) {
                _context.RoomTypes.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Fee>> GetAllFeesAsync() => await _context.Fees.ToListAsync();
    }
}

