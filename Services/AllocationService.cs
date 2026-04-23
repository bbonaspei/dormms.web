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
        private readonly IAuditService _audit;
        private readonly INotificationService _notificationService;

        public AllocationService(IAllocationRepository repo, ApplicationDbContext context, IAuditService audit, INotificationService notificationService)
        {
            _repo = repo;
            _context = context;
            _audit = audit;
            _notificationService = notificationService;
        }

        public async Task<bool> CreateAllocationAsync(int studentId, int roomId, DateTime startDate, DateTime endDate, decimal deposit, string keyCard)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null || room.currentOccupancy >= room.capacity || room.status == "Occupied" || room.status == "Maintenance") return false;

            var existing = await GetStudentActiveAllocationAsync(studentId);
            if (existing != null) return false;

            var allocation = new Allocation
            {
                studentId = studentId,
                roomId = roomId,
                startDate = startDate,
                endDate = endDate,
                securityDeposit = deposit,
                keyCardNumber = keyCard,
                status = "Confirmed",
                isCurrent = true
            };

            room.currentOccupancy += 1;
            if (room.currentOccupancy == room.capacity) room.status = "Occupied";
            _context.Rooms.Update(room);

            var student = await _context.Students.FindAsync(studentId);
            if (student != null)
            {
                student.roomId = roomId;
                _context.Students.Update(student);
            }

            await _context.Allocations.AddAsync(allocation);
            await _context.SaveChangesAsync();

            await _audit.LogActionAsync("CREATE", "Allocation", allocation.id, null, $"Student #{studentId} assigned to Room #{roomId}");

            await _notificationService.SendNotificationToStudentAsync(studentId, "Room Allocated", $"You have been assigned to Room #{roomId}.", "Success", "/Home/Index");

            return true;
        }

        public async Task<IEnumerable<Allocation>> GetActiveAllocationsAsync() => await _repo.GetAllAsync();

        public async Task<bool> ProcessAllocationAsync(Allocation allocation)
        {
            return await CreateAllocationAsync(allocation.studentId, allocation.roomId, allocation.startDate, allocation.endDate ?? DateTime.Now.AddYears(1), allocation.securityDeposit, allocation.keyCardNumber ?? "");
        }
        public async Task<bool> TerminateAllocationAsync(int id)
        {

            var allocation = await _context.Allocations
                .Include(a => a.Room)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(x => x.id == id);

            if (allocation == null) return false;

            allocation.actualCheckOut = DateTime.Now;
            allocation.isCurrent = false;
            allocation.status = "Completed";

            if (allocation.Room != null)
            {
                allocation.Room.currentOccupancy--;
                if (allocation.Room.status == "Occupied")
                    allocation.Room.status = "Available";
            }

            if (allocation.Student != null)
            {
                allocation.Student.roomId = null;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RequestAllocationAsync(int studentId, int roomId)
        {

            var existing = await GetStudentActiveAllocationAsync(studentId);
            if (existing != null) return false;

            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null || room.currentOccupancy >= room.capacity || room.status == "Occupied" || room.status == "Maintenance") return false;

            var request = new Allocation
            {
                studentId = studentId,
                roomId = roomId,
                startDate = DateTime.Now,
                endDate = DateTime.Now.AddYears(1),
                status = "Pending",
                isCurrent = true,
                createdAt = DateTime.Now
            };

            await _context.Allocations.AddAsync(request);
            await _context.SaveChangesAsync();

            await _audit.LogActionAsync("REQUEST", "Allocation", request.id, null, $"Student #{studentId} requested Room #{roomId}");
            return true;
        }

        public async Task<Allocation?> GetStudentActiveAllocationAsync(int studentId)
        {
            return await _context.Allocations
                .FirstOrDefaultAsync(a => a.studentId == studentId && a.isCurrent && (a.status == "Confirmed" || a.status == "Checked-In" || a.status == "Pending"));
        }
    }
}

