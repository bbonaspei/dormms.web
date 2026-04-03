using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using DormMS.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Services
{
    public class AllocationService : IAllocationService
    {
        private readonly IAllocationRepository _repo;
        private readonly ApplicationDbContext _context;

        public AllocationService(IAllocationRepository repo, ApplicationDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        // GÖREV 1: Diyagramdaki ID bazlı atama süreci
        public async Task<bool> CreateAllocationAsync(int studentId, int roomId, DateTime startDate, DateTime endDate)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null || room.status != "Available") return false;

            var allocation = new Allocation
            {
                studentId = studentId,
                roomId = roomId,
                startDate = startDate,
                endDate = endDate,
                status = "Confirmed",
                isCurrent = true
            };

            room.status = "Occupied";
            room.currentOccupancy += 1;

            await _repo.AddAsync(allocation);
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
            return true;
        }

        // GÖREV 2: Aktif atamaları listeleme
        public async Task<IEnumerable<Allocation>> GetActiveAllocationsAsync()
        {
            return await _repo.GetAllAsync();
        }

        // GÖREV 3: Nesne bazlı atama süreci (GÖREV 1'i kullanarak işi bitirir)
        public async Task<bool> ProcessAllocationAsync(Allocation allocation)
        {
            // Bu metot, yukarıdaki GÖREV 1'i çağırarak kod tekrarını önler
            return await CreateAllocationAsync(
                allocation.studentId,
                allocation.roomId,
                allocation.startDate,
                allocation.endDate ?? DateTime.Now.AddYears(1)
            );
        }
    }
}