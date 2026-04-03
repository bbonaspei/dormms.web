using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly ApplicationDbContext _context;
        public MaintenanceService(ApplicationDbContext context) { _context = context; }

        public async Task<IEnumerable<MaintenanceRequest>> GetAllRequestsAsync()
        {
            return await _context.MaintenanceRequests
                .Include(r => r.Student).ThenInclude(s => s.User)
                .Include(r => r.Room)
                .OrderByDescending(r => r.requestDate)
                .ToListAsync();
        }

        public async Task CreateRequestAsync(MaintenanceRequest request)
        {
            request.requestNumber = "REQ-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            request.requestDate = DateTime.Now;
            request.status = "Pending";

            _context.MaintenanceRequests.Add(request);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(int requestId, string newStatus)
        {
            var request = await _context.MaintenanceRequests.FindAsync(requestId);
            if (request != null)
            {
                request.status = newStatus;
                if (newStatus == "Completed") request.completedDate = DateTime.Now;
                _context.MaintenanceRequests.Update(request);
                await _context.SaveChangesAsync();
            }
        }
    }
}