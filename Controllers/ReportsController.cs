using Microsoft.AspNetCore.Mvc;
using DormMS.Web.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DormMS.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> OccupancyReport()
        {
            var rooms = await _context.Rooms.Include(r => r.Building).ToListAsync();
            var csv = new StringBuilder();
            csv.AppendLine("Room Number,Building,Capacity,Occupancy,Status");
            foreach (var room in rooms)
            {
                csv.AppendLine($"{room.roomNumber},{room.Building?.buildingName ?? "N/A"},{room.capacity},{room.currentOccupancy},{room.status}");
            }
            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "OccupancyReport.csv");
        }

        public async Task<IActionResult> FinancialReport()
        {
            var payments = await _context.Payments.Include(p => p.Student).ThenInclude(s => s.User).ToListAsync();
            var csv = new StringBuilder();
            csv.AppendLine("Student Name,Amount,Status,Date,Transaction ID");
            foreach (var p in payments)
            {
                string name = p.Student?.User != null ? $"{p.Student.User.firstName} {p.Student.User.lastName}" : "N/A";
                csv.AppendLine($"{name},{p.amount},{p.status},{p.paymentDate},{p.transactionId}");
            }
            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "FinancialReport.csv");
        }

        public async Task<IActionResult> MaintenanceReport()
        {
            var requests = await _context.MaintenanceRequests.Include(r => r.Room).ToListAsync();
            var csv = new StringBuilder();
            csv.AppendLine("Room,Issue,Priority,Status,Date");
            foreach (var r in requests)
            {
                string desc = r.description?.Replace(",", " ") ?? "None";
                csv.AppendLine($"{r.Room?.roomNumber ?? "N/A"},{desc},{r.priority},{r.status},{r.requestDate}");
            }
            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "MaintenanceReport.csv");
        }
    }
}

