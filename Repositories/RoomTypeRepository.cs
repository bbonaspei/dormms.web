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

        public async Task<IEnumerable<RoomType>> GetAllAsync() => await _context.RoomTypes.ToListAsync();
        public async Task AddAsync(RoomType roomType) { await _context.RoomTypes.AddAsync(roomType); await _context.SaveChangesAsync(); }
    }
}