using Microsoft.AspNetCore.Mvc;
using DormMS.Web.Data;
using DormMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Controllers
{
    public class SeedDataController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeedDataController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // 1. Ek Binalar
            if (!await _context.Buildings.AnyAsync(b => b.buildingCode == "B"))
            {
                _context.Buildings.Add(new Building { buildingName = "Beta Block", buildingCode = "B", totalFloors = 3, hasElevator = false, status = "Active" });
                _context.Buildings.Add(new Building { buildingName = "Gamma Block", buildingCode = "G", totalFloors = 5, hasElevator = true, status = "Active" });
            }

            // 2. Ek Odalar (Geliştirilmiş Kontrol)
            if (!await _context.Rooms.AnyAsync(r => r.roomNumber == "102"))
            {
                _context.Rooms.Add(new Room { roomNumber = "102", buildingId = 1, roomTypeId = 2, floorNumber = 1, capacity = 2, currentOccupancy = 0, status = "Available" });
            }
            if (!await _context.Rooms.AnyAsync(r => r.roomNumber == "201"))
            {
                _context.Rooms.Add(new Room { roomNumber = "201", buildingId = 1, roomTypeId = 1, floorNumber = 2, capacity = 1, currentOccupancy = 0, status = "Available" });
            }
            if (!await _context.Rooms.AnyAsync(r => r.roomNumber == "B-101"))
            {
                _context.Rooms.Add(new Room { roomNumber = "B-101", buildingId = 1, roomTypeId = 1, floorNumber = 1, capacity = 1, currentOccupancy = 0, status = "Available" });
            }

            // 3. Ek Kullanıcılar ve Öğrenciler
            if (await _context.Users.CountAsync() < 10)
            {
                var students = new[] {
                    new { U = "student2", F = "Michael", L = "Smith", S = "STU002" },
                    new { U = "student3", F = "Sarah", L = "Johnson", S = "STU003" },
                    new { U = "student4", F = "David", L = "Williams", S = "STU004" },
                    new { U = "student5", F = "Jessica", L = "Brown", S = "STU005" }
                };

                foreach (var s in students)
                {
                    var user = new User
                    {
                        username = s.U, email = $"{s.U}@dorm.com", firstName = s.F, lastName = s.L,
                        passwordHash = "123", isActive = true, createdAt = DateTime.Now, updatedAt = DateTime.Now
                    };
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    _context.Students.Add(new Student { userId = user.Id, studentId = s.S, status = "Active", createdAt = DateTime.Now, updatedAt = DateTime.Now });
                    _context.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = 3 });
                }
            }

            // 4. Bakım Talepleri (Broadcast ve Specific)
            if (await _context.MaintenanceRequests.CountAsync() < 3)
            {
                // BROADCAST (Admin Duyurusu)
                _context.MaintenanceRequests.Add(new MaintenanceRequest
                {
                    requestNumber = "ANNC-001",
                    issueCategory = "Utility",
                    description = "Main elevator in Alpha Block will be under maintenance tomorrow between 10:00-12:00.",
                    priority = "Medium",
                    status = "Pending",
                    requestDate = DateTime.Now.AddHours(-2),
                    studentId = null, // BROADCAST
                    roomId = null     // BROADCAST
                });

                _context.MaintenanceRequests.Add(new MaintenanceRequest
                {
                    requestNumber = "ANNC-002",
                    issueCategory = "Plumbing",
                    description = "Scheduled water maintenance in the whole campus on Sunday.",
                    priority = "High",
                    status = "Pending",
                    requestDate = DateTime.Now.AddHours(-1),
                    studentId = null,
                    roomId = null
                });

                // ÖĞRENCİYE ÖZEL
                _context.MaintenanceRequests.Add(new MaintenanceRequest
                {
                    requestNumber = "REQ-SPEC001",
                    issueCategory = "Electrical",
                    description = "Socket not working near the bed.",
                    priority = "Low",
                    status = "Pending",
                    requestDate = DateTime.Now.AddDays(-1),
                    studentId = 1,
                    roomId = 1
                });
            }

            // 5. Finansal Veriler (Borçlar)
            if (await _context.StudentFees.CountAsync() < 10)
            {
                var students = await _context.Students.Take(5).ToListAsync();
                foreach (var s in students)
                {
                    _context.StudentFees.Add(new StudentFee { studentId = s.id, feeId = 1, amount = 850, status = "Unpaid", dueDate = DateTime.Now.AddDays(-5) });
                    _context.StudentFees.Add(new StudentFee { studentId = s.id, feeId = 1, amount = 850, status = "Paid", dueDate = DateTime.Now.AddMonths(-1) });
                }
            }

            await _context.SaveChangesAsync();
            return Content("Success: Data seeded. Go back to Home.");
        }
    }
}
