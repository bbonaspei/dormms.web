using Microsoft.EntityFrameworkCore;
using DormMS.Web.Models;

namespace DormMS.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Allocation> Allocations { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<StudentFee> StudentFees { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }

        // KRİTİK EKSİK BURASIYDI:
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<StudentDocument> StudentDocuments { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<Document> Documents { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ara tablo anahtarları
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<RolePermission>().HasKey(rp => new { rp.RoleId, rp.PermissionId });

            // --- DATA SEEDING (Büyük harf uyuşmazlıkları giderildi) ---
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = "Admin", Description = "Sistem yöneticisi" },
                new Role { Id = 2, RoleName = "DormManager", Description = "Yurt müdürü" },
                new Role { Id = 3, RoleName = "Student", Description = "Öğrenci" },
                new Role { Id = 4, RoleName = "Staff", Description = "Personel / Tekniker" },
                new Role { Id = 5, RoleName = "Finance", Description = "Mali İşler Personeli" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1, 
                    username = "admin",
                    email = "admin@dorm.com",
                    firstName = "System",
                    lastName = "Admin",
                    passwordHash = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", // SHA256 Hash of "123"
                    isActive = true,
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                },
                new User
                {
                    Id = 2,
                    username = "staff",
                    email = "staff@dorm.com",
                    firstName = "John",
                    lastName = "Staff",
                    passwordHash = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", // SHA256 Hash of "123"
                    isActive = true,
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                },
                new User
                {
                    Id = 3,
                    username = "student",
                    email = "student@dorm.com",
                    firstName = "Emily",
                    lastName = "Resident",
                    passwordHash = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", // SHA256 Hash of "123"
                    isActive = true,
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    id = 1,
                    userId = 3,
                    studentId = "STU001",
                    status = "Active",
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                }
            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserId = 1, RoleId = 1 },
                new UserRole { UserId = 2, RoleId = 4 },
                new UserRole { UserId = 3, RoleId = 3 }
            );

            modelBuilder.Entity<Building>().HasData(
                new Building { id = 1, buildingName = "Alpha Block", buildingCode = "A", totalFloors = 4, hasElevator = true, status = "Active" }
            );

            modelBuilder.Entity<RoomType>().HasData(
                new RoomType { id = 1, typeName = "Executive Single", capacity = 1, bedCount = 1, basePrice = 1200, hasBathroom = true, description = "Premium privacy and high-end comfort." },
                new RoomType { id = 2, typeName = "Collaborative Twin", capacity = 2, bedCount = 2, basePrice = 950, hasBathroom = true, description = "Perfect for friends and shared living." },
                new RoomType { id = 3, typeName = "Triple Shared", capacity = 3, bedCount = 3, basePrice = 750, hasBathroom = true, description = "Spacious shared living for groups." },
                new RoomType { id = 4, typeName = "Standard Quad", capacity = 4, bedCount = 4, basePrice = 600, hasBathroom = false, description = "Economical shared housing." },
                new RoomType { id = 5, typeName = "Budget Shared", capacity = 6, bedCount = 6, basePrice = 450, hasBathroom = false, description = "Affordable community living." }
            );

            modelBuilder.Entity<Room>().HasData(
                new Room { id = 1, roomNumber = "101", buildingId = 1, roomTypeId = 1, floorNumber = 1, capacity = 1, currentOccupancy = 1, status = "Occupied" }
            );

            modelBuilder.Entity<Allocation>().HasData(
                new Allocation 
                { 
                    id = 1, 
                    studentId = 1, 
                    roomId = 1, 
                    startDate = DateTime.Now.AddMonths(-2), 
                    isCurrent = true, 
                    status = "Checked-In", 
                    securityDeposit = 1000 
                }
            );

            modelBuilder.Entity<Fee>().HasData(
                new Fee { id = 1, feeName = "Monthly Rent", amount = 850, isRecurring = true, feeCategory = "Accommodation" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}