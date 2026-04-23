using DormMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Allocation> Allocations { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<StudentFee> StudentFees { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<StudentDocument> StudentDocuments { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<RolePermission>().HasKey(rp => new { rp.RoleId, rp.PermissionId });
            modelBuilder.Entity<SystemSetting>().HasKey(s => s.settingKey);

            
            modelBuilder.Entity<Fee>().Property(f => f.amount).HasPrecision(18, 2);
            modelBuilder.Entity<StudentFee>().Property(sf => sf.amount).HasPrecision(18, 2);
            modelBuilder.Entity<Payment>().Property(p => p.amount).HasPrecision(18, 2);
            modelBuilder.Entity<Penalty>().Property(p => p.amount).HasPrecision(18, 2);
            modelBuilder.Entity<RoomType>().Property(rt => rt.basePrice).HasPrecision(18, 2);
            modelBuilder.Entity<Allocation>().Property(a => a.securityDeposit).HasPrecision(18, 2);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.userId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = "Admin", Description = "Sistem Yöneticisi" },
                new Role { Id = 2, RoleName = "DormManager", Description = "Yurt Müdürü" },
                new Role { Id = 3, RoleName = "Student", Description = "Öğrenci" },
                new Role { Id = 4, RoleName = "Staff", Description = "Personel / Tekniker" },
                new Role { Id = 5, RoleName = "Finance", Description = "Mali İşler Personeli" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, username = "admin", email = "admin@dorm.com", firstName = "Sistem", lastName = "Yöneticisi", passwordHash = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", isActive = true, createdAt = DateTime.Now, updatedAt = DateTime.Now },
                new User { Id = 2, username = "staff", email = "personel@dorm.com", firstName = "Mehmet", lastName = "Tekniker", passwordHash = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", isActive = true, createdAt = DateTime.Now, updatedAt = DateTime.Now },
                new User { Id = 3, username = "student", email = "ayse@email.com", firstName = "Ayşe", lastName = "Demir", passwordHash = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", isActive = true, createdAt = DateTime.Now, updatedAt = DateTime.Now },
                new User { Id = 4, username = "ahmet", email = "ahmet@email.com", firstName = "Ahmet", lastName = "Yılmaz", passwordHash = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", isActive = true, createdAt = DateTime.Now, updatedAt = DateTime.Now },
                new User { Id = 5, username = "fatma", email = "fatma@email.com", firstName = "Fatma", lastName = "Kaya", passwordHash = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", isActive = true, createdAt = DateTime.Now, updatedAt = DateTime.Now },
                new User { Id = 6, username = "can", email = "can@email.com", firstName = "Can", lastName = "Yıldız", passwordHash = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", isActive = true, createdAt = DateTime.Now, updatedAt = DateTime.Now },
                new User { Id = 7, username = "elif", email = "elif@email.com", firstName = "Elif", lastName = "Şahin", passwordHash = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", isActive = true, createdAt = DateTime.Now, updatedAt = DateTime.Now }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { id = 1, userId = 3, studentId = "STU001", status = "Active", createdAt = DateTime.Now, updatedAt = DateTime.Now },
                new Student { id = 2, userId = 4, studentId = "STU002", status = "Active", createdAt = DateTime.Now, updatedAt = DateTime.Now },
                new Student { id = 3, userId = 5, studentId = "STU003", status = "Active", createdAt = DateTime.Now, updatedAt = DateTime.Now },
                new Student { id = 4, userId = 6, studentId = "STU004", status = "Active", createdAt = DateTime.Now, updatedAt = DateTime.Now },
                new Student { id = 5, userId = 7, studentId = "STU005", status = "Active", createdAt = DateTime.Now, updatedAt = DateTime.Now }
            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserId = 1, RoleId = 1 },
                new UserRole { UserId = 2, RoleId = 4 },
                new UserRole { UserId = 3, RoleId = 3 },
                new UserRole { UserId = 4, RoleId = 3 },
                new UserRole { UserId = 5, RoleId = 3 },
                new UserRole { UserId = 6, RoleId = 3 },
                new UserRole { UserId = 7, RoleId = 3 }
            );

            modelBuilder.Entity<Building>().HasData(
                new Building { id = 1, buildingName = "A Blok (Merkez)", buildingCode = "A", totalFloors = 4, hasElevator = true, status = "Active" },
                new Building { id = 2, buildingName = "B Blok (Ek Bina)", buildingCode = "B", totalFloors = 3, hasElevator = false, status = "Active" }
            );

            modelBuilder.Entity<RoomType>().HasData(
                new RoomType { id = 1, typeName = "VİP Tek Kişilik", capacity = 1, bedCount = 1, basePrice = 1200, hasBathroom = true, description = "Yüksek konfor ve gizlilik." },
                new RoomType { id = 2, typeName = "Standart Çift Kişilik", capacity = 2, bedCount = 2, basePrice = 950, hasBathroom = true, description = "Arkadaşlar için ideal paylaşımlı yaşam." },
                new RoomType { id = 3, typeName = "Üç Kişilik Oda", capacity = 3, bedCount = 3, basePrice = 750, hasBathroom = true, description = "Geniş ve sosyal oda seçeneği." },
                new RoomType { id = 4, typeName = "Dört Kişilik Oda", capacity = 4, bedCount = 4, basePrice = 600, hasBathroom = false, description = "Ekonomik paylaşımlı konaklama." },
                new RoomType { id = 5, typeName = "Ekonomik Koğuş", capacity = 6, bedCount = 6, basePrice = 450, hasBathroom = false, description = "Bütçe dostu toplu yaşam." }
            );

            modelBuilder.Entity<Room>().HasData(
                new Room { id = 1, roomNumber = "101", buildingId = 1, roomTypeId = 1, floorNumber = 1, capacity = 1, currentOccupancy = 1, status = "Occupied" },
                new Room { id = 2, roomNumber = "102", buildingId = 1, roomTypeId = 2, floorNumber = 1, capacity = 2, currentOccupancy = 1, status = "Available" },
                new Room { id = 3, roomNumber = "103", buildingId = 1, roomTypeId = 3, floorNumber = 1, capacity = 3, currentOccupancy = 0, status = "Available" },
                new Room { id = 4, roomNumber = "201", buildingId = 2, roomTypeId = 2, floorNumber = 2, capacity = 2, currentOccupancy = 1, status = "Available" },
                new Room { id = 5, roomNumber = "202", buildingId = 2, roomTypeId = 4, floorNumber = 2, capacity = 4, currentOccupancy = 0, status = "Available" }
            );

            modelBuilder.Entity<Allocation>().HasData(
                new Allocation { id = 1, studentId = 1, roomId = 1, startDate = DateTime.Now.AddMonths(-2), isCurrent = true, status = "Checked-In", securityDeposit = 1000 },
                new Allocation { id = 2, studentId = 2, roomId = 2, startDate = DateTime.Now.AddMonths(-1), isCurrent = true, status = "Checked-In", securityDeposit = 800 },
                new Allocation { id = 3, studentId = 3, roomId = 4, startDate = DateTime.Now.AddDays(-10), isCurrent = true, status = "Checked-In", securityDeposit = 800 }
            );

            modelBuilder.Entity<Fee>().HasData(
                new Fee { id = 1, feeName = "Aylık Kira", amount = 1200, isRecurring = true, feeCategory = "Konaklama" },
                new Fee { id = 2, feeName = "Yemek Bedeli", amount = 450, isRecurring = true, feeCategory = "Beslenme" }
            );

            modelBuilder.Entity<MaintenanceRequest>().HasData(
                new MaintenanceRequest { id = 1, requestNumber = "MR-001", studentId = 1, roomId = 1, issueCategory = "Elektrik", description = "Klima kumandası çalışmıyor.", priority = "High", status = "Pending", requestDate = DateTime.Now.AddDays(-2) },
                new MaintenanceRequest { id = 2, requestNumber = "MR-002", studentId = 2, roomId = 2, issueCategory = "Mobilya", description = "Dolap kapağı sallanıyor.", priority = "Medium", status = "In Progress", requestDate = DateTime.Now.AddDays(-5) },
                new MaintenanceRequest { id = 3, requestNumber = "MR-003", studentId = 3, roomId = 4, issueCategory = "Tesisat", description = "Musluk sökülmüş.", priority = "Low", status = "Completed", requestDate = DateTime.Now.AddDays(-10), completedDate = DateTime.Now.AddDays(-8) }
            );

            modelBuilder.Entity<Payment>().HasData(
                new Payment { id = 1, studentId = 1, amount = 1200, paymentMethod = "Credit Card", paymentDate = DateTime.Now.AddDays(-5), status = "Completed", transactionId = "TXN1001" },
                new Payment { id = 2, studentId = 2, amount = 950, paymentMethod = "Bank Transfer", paymentDate = DateTime.Now.AddDays(-3), status = "Completed", transactionId = "TXN1002" }
            );

            modelBuilder.Entity<AuditLog>().HasData(
                new AuditLog { id = 1, action = "SEED", entityType = "System", entityId = 0, createdAt = DateTime.Now, newValues = "Initial system seeding completed." }
            );
        }
    }
}

