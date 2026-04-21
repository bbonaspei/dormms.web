using Microsoft.EntityFrameworkCore;
using DormMS.Web.Data;
using DormMS.Web.Models;
using DormMS.Web.Interfaces;
using System.Security.Principal;

namespace DormMS.Web.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly ApplicationDbContext _context;
        public BuildingRepository(ApplicationDbContext context) { _context = context; }

        public async Task<IEnumerable<Building>> GetAllAsync() => await _context.Buildings.ToListAsync();
        public async Task<Building?> GetByIdAsync(int id) => await _context.Buildings.FindAsync(id);
        public async Task AddAsync(Building building) { await _context.Buildings.AddAsync(building); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Building building) { _context.Buildings.Update(building); await _context.SaveChangesAsync();}
        public async Task DeleteAsync(int id)
        {
            var building = await _context.Buildings.FindAsync(id);
            if (building != null) { _context.Buildings.Remove(building); await _context.SaveChangesAsync(); }
        }
        
    }
}
