using DormMS.Web.Interfaces;
using DormMS.Web.Models;

namespace DormMS.Web.Services
{
    public class FinancialService : IFinancialService
    {
        private readonly IPaymentRepository _paymentRepo;
        private readonly IStudentFeeRepository _studentFeeRepo;
        private readonly IAllocationRepository _allocationRepo;
        private readonly IRoomRepository _roomRepo;
        private readonly INotificationService _notificationService;
        private readonly IAuditService _audit;
        private readonly IPenaltyRepository _penaltyRepo;

        public FinancialService(
            IPaymentRepository paymentRepo,
            IStudentFeeRepository studentFeeRepo,
            IAllocationRepository allocationRepo,
            IRoomRepository roomRepo,
            INotificationService notificationService,
            IAuditService audit,
            IPenaltyRepository penaltyRepo)
        {
            _paymentRepo = paymentRepo;
            _studentFeeRepo = studentFeeRepo;
            _allocationRepo = allocationRepo;
            _roomRepo = roomRepo;
            _notificationService = notificationService;
            _audit = audit;
            _penaltyRepo = penaltyRepo;
        }

        public async Task<bool> ProcessPaymentAsync(Payment payment, int feeId)
        {
            payment.paymentReference = "RCPT-" + DateTime.Now.Ticks.ToString().Substring(10);
            payment.transactionId = "TXN-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            
            await _paymentRepo.AddPaymentAsync(payment);

            var fee = await _studentFeeRepo.GetByIdAsync(feeId);
            if (fee != null)
            {
                fee.status = "Paid";
                await _studentFeeRepo.UpdateAsync(fee);

                var penalties = await _penaltyRepo.GetByStudentIdAsync(payment.studentId);
                var feePenalties = penalties.Where(p => p.reason != null && p.reason.Contains($"Fee #{feeId}")).ToList();
                foreach (var p in feePenalties)
                {
                    p.status = "Paid";
                    await _penaltyRepo.UpdatePenaltyAsync(p); 
                }
            }

            await _notificationService.SendNotificationToStudentAsync(payment.studentId, "Payment Success", $"Amount: TL{payment.amount}", "Success");

            await _audit.LogActionAsync("CREATE", "Payment", payment.id, null, $"Collected TL{payment.amount}");

            return true;
        }

        public async Task CreateRoomChargeAsync(int studentId, int roomId)
        {
            var room = await _roomRepo.GetRoomWithDetailsAsync(roomId);
            if (room?.RoomType != null)
            {
                var roomType = room.RoomType;
                var charge = new StudentFee
                {
                    studentId = studentId,
                    amount = roomType.basePrice,
                    feeId = 1,
                    status = "Unpaid",
                    dueDate = DateTime.Now.AddDays(7)
                };
                await _studentFeeRepo.AddAsync(charge);

                await _audit.LogActionAsync("CREATE", "Fee", charge.id, null, $"Rent charge created for Student #{studentId}");
            }
        }

        public async Task<IEnumerable<Payment>> GetAllTransactionsAsync() => await _paymentRepo.GetAllPaymentsAsync();
        
        public async Task<IEnumerable<StudentFee>> GetOutstandingDuesAsync(int studentId) 
            => (await _studentFeeRepo.GetByStudentIdAsync(studentId)).Where(f => f.status == "Unpaid");

        public async Task SyncStudentChargesAsync(int studentId)
        {

            var allocation = await _allocationRepo.GetActiveByStudentIdAsync(studentId);

            if (allocation == null) return;

            var startDate = allocation.startDate;
            var endDate = allocation.endDate ?? DateTime.Now;
            var checkLimit = DateTime.Now > endDate ? endDate : DateTime.Now;

            var existingCharges = await _studentFeeRepo.GetByStudentIdAsync(studentId);
            var rentCharges = existingCharges.Where(f => f.feeId == 1);

            var tempDate = new DateTime(startDate.Year, startDate.Month, 1);
            var limitDate = new DateTime(checkLimit.Year, checkLimit.Month, 1);

            while (tempDate <= limitDate)
            {

                bool hasCharge = rentCharges.Any(f => f.dueDate.Year == tempDate.Year && f.dueDate.Month == tempDate.Month);

                if (!hasCharge)
                {
                    var newCharge = new StudentFee
                    {
                        studentId = studentId,
                        feeId = allocation.Room?.RoomType?.feeId ?? 1,
                        amount = allocation.Room?.RoomType?.basePrice ?? 0,
                        status = "Unpaid",
                        dueDate = new DateTime(tempDate.Year, tempDate.Month, 1).AddDays(4)
                    };
                    await _studentFeeRepo.AddAsync(newCharge);
                    
                    await _audit.LogActionAsync("CREATE", "AutoFee", newCharge.id, null, $"Automated rent charge for {tempDate:MMMM yyyy} created for Student #{studentId}");
                }

                tempDate = tempDate.AddMonths(1);
            }

            await CalculateLatePenaltiesAsync();
        }

        public async Task CalculateLatePenaltiesAsync()
        {
            var overdueFees = (await _studentFeeRepo.GetAllAsync())
                              .Where(f => f.status == "Unpaid" && f.dueDate < DateTime.Now);

            foreach (var fee in overdueFees)
            {

                var daysOverdue = (DateTime.Now - fee.dueDate).Days;
                var weeksOverdue = (int)Math.Floor((double)daysOverdue / 7);

                if (weeksOverdue > 0)
                {
                    var studentPenalties = await _penaltyRepo.GetByStudentIdAsync(fee.studentId);
                    
                    for (int i = 1; i <= weeksOverdue; i++)
                    {
                        string weekReason = $"Week {i} Late Payment Penalty for Fee #{fee.id}";
                        bool alreadyApplied = studentPenalties.Any(p => p.reason == weekReason);

                        if (!alreadyApplied)
                        {
                            var penalty = new Penalty
                            {
                                studentId = fee.studentId,
                                penaltyType = "Late Fee",
                                amount = fee.amount * 0.05m,
                                appliedDate = DateTime.Now,
                                reason = weekReason,
                                status = "Pending"
                            };
                            await _penaltyRepo.AddPenaltyAsync(penalty);

                            await _notificationService.SendNotificationToStudentAsync(
                                fee.studentId, 
                                "New Penalty Applied", 
                                $"A 5% late fee penalty (Week {i}) has been added due to overdue payment.", 
                                "Warning");

                            await _audit.LogActionAsync("CREATE", "Penalty", penalty.id, null, $"Weekly penalty added for Student #{fee.studentId}");
                        }
                    }
                }
            }
        }
    }
}

