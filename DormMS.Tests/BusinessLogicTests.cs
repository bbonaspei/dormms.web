using Xunit;
using Moq;
using DormMS.Web.Services;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using DormMS.Web.Data;
using System.Linq;

namespace DormMS.Tests
{
    public class BusinessLogicTests
    {
        [Fact]
        public async Task ProcessPaymentAsync_Should_Call_AddPayment_And_UpdateNotification()
        {
            // Arrange
            var mockPaymentRepo = new Mock<IPaymentRepository>();
            var mockFeeRepo = new Mock<IStudentFeeRepository>();
            var mockAllocRepo = new Mock<IAllocationRepository>();
            var mockRoomRepo = new Mock<IRoomRepository>();
            var mockNotifService = new Mock<INotificationService>();
            var mockAudit = new Mock<IAuditService>();
            var mockPenaltyRepo = new Mock<IPenaltyRepository>();

            var service = new FinancialService(
                mockPaymentRepo.Object,
                mockFeeRepo.Object,
                mockAllocRepo.Object,
                mockRoomRepo.Object,
                mockNotifService.Object,
                mockAudit.Object,
                mockPenaltyRepo.Object
            );

            var payment = new Payment { studentId = 1, amount = 100 };
            var feeId = 10;

            // Act
            var result = await service.ProcessPaymentAsync(payment, feeId);

            // Assert
            Assert.True(result);
            mockPaymentRepo.Verify(r => r.AddPaymentAsync(It.IsAny<Payment>()), Times.Once);
            mockNotifService.Verify(s => s.SendNotificationAsync(1, "Payment Success", It.IsAny<string>(), "Success", "#"), Times.Once);
        }

        [Fact]
        public async Task GetActiveAllocationsAsync_Should_Return_Allocations_From_Repo()
        {
            // Arrange
            var mockRepo = new Mock<IAllocationRepository>();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Allocation")
                .Options;
            
            using var context = new ApplicationDbContext(options);
            var mockAudit = new Mock<IAuditService>();
            var mockNotif = new Mock<INotificationService>();

            var expected = new System.Collections.Generic.List<Allocation> { new Allocation { id = 1, studentId = 1 } };
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(expected);

            var service = new AllocationService(mockRepo.Object, context, mockAudit.Object, mockNotif.Object);

            // Act
            var result = await service.GetActiveAllocationsAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result.First().id);
        }

        [Fact]
        public async Task EnrollNewStudentAsync_Should_Create_User_And_Student()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Enroll")
                .Options;

            using var context = new ApplicationDbContext(options);
            var mockRepo = new Mock<IStudentRepository>();
            var mockAudit = new Mock<IAuditService>();
            var mockNotif = new Mock<INotificationService>();

            var service = new StudentService(mockRepo.Object, context, mockAudit.Object, mockNotif.Object);

            var student = new Student { studentId = "TEST001" };

            // Act
            await service.EnrollNewStudentAsync(student, "John", "Doe", "john@test.com");

            // Assert
            var user = await context.Users.FirstOrDefaultAsync(u => u.username == "TEST001");
            Assert.NotNull(user);
            Assert.Equal("John", user.firstName);
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Student>()), Times.Once);
            mockAudit.Verify(a => a.LogActionAsync("CREATE", "Student", It.IsAny<int>(), null, It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task SyncStudentChargesAsync_Should_Create_Missing_Charges()
        {
            // Arrange
            var mockPaymentRepo = new Mock<IPaymentRepository>();
            var mockFeeRepo = new Mock<IStudentFeeRepository>();
            var mockAllocRepo = new Mock<IAllocationRepository>();
            var mockRoomRepo = new Mock<IRoomRepository>();
            var mockNotifService = new Mock<INotificationService>();
            var mockAudit = new Mock<IAuditService>();
            var mockPenaltyRepo = new Mock<IPenaltyRepository>();

            var service = new FinancialService(
                mockPaymentRepo.Object,
                mockFeeRepo.Object,
                mockAllocRepo.Object,
                mockRoomRepo.Object,
                mockNotifService.Object,
                mockAudit.Object,
                mockPenaltyRepo.Object
            );

            var studentId = 1;
            var allocation = new Allocation 
            { 
                studentId = studentId, 
                startDate = DateTime.Now.AddMonths(-1),
                isCurrent = true,
                Room = new Room { RoomType = new RoomType { basePrice = 500 } }
            };

            mockAllocRepo.Setup(r => r.GetActiveByStudentIdAsync(studentId)).ReturnsAsync(allocation);
            mockFeeRepo.Setup(r => r.GetByStudentIdAsync(studentId)).ReturnsAsync(new System.Collections.Generic.List<StudentFee>());

            // Act
            await service.SyncStudentChargesAsync(studentId);

            // Assert
            mockFeeRepo.Verify(r => r.AddAsync(It.IsAny<StudentFee>()), Times.AtLeastOnce);
        }
    }
}
