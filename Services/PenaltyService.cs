using DormMS.Web.Interfaces;
using DormMS.Web.Models;

namespace DormMS.Web.Services
{
    public class PenaltyService : IPenaltyService
    {
        private readonly IPenaltyRepository _penaltyRepo;
        private readonly IAuditService _audit;
        private readonly INotificationService _notificationService;

        public PenaltyService(IPenaltyRepository penaltyRepo, IAuditService audit, INotificationService notificationService)
        {
            _penaltyRepo = penaltyRepo;
            _audit = audit;
            _notificationService = notificationService;
        }

        public async Task<IEnumerable<Penalty>> GetPenaltiesAsync()
        {
            return await _penaltyRepo.GetAllPenaltiesAsync();
        }

        public async Task<IEnumerable<Penalty>> GetStudentPenaltiesAsync(int studentId)
        {
            return await _penaltyRepo.GetByStudentIdAsync(studentId);
        }

        public async Task<Penalty?> GetPenaltyByIdAsync(int id)
        {
            return await _penaltyRepo.GetPenaltyByIdAsync(id);
        }

        public async Task<bool> ApplyPenaltyAsync(Penalty penalty)
        {
            if (penalty.appliedDate == default) penalty.appliedDate = DateTime.Now;
            if (string.IsNullOrEmpty(penalty.status)) penalty.status = "Pending";

            var result = await _penaltyRepo.AddPenaltyAsync(penalty);
            if (result)
            {
                await _audit.LogActionAsync("CREATE", "Penalty", penalty.id, null, $"Manual penalty applied: {penalty.penaltyType}");
                await _notificationService.SendNotificationToStudentAsync(penalty.studentId, "Penalty Applied", $"A penalty of ${penalty.amount} has been applied to your account.", "Warning");
            }
            return result;
        }

        public async Task<bool> ResolvePenaltyAsync(int penaltyId)
        {
            var penalty = await _penaltyRepo.GetPenaltyByIdAsync(penaltyId);
            if (penalty != null)
            {
                penalty.status = "Paid";
                var result = await _penaltyRepo.UpdatePenaltyAsync(penalty);
                if (result)
                {
                    await _audit.LogActionAsync("UPDATE", "Penalty", penalty.id, null, "Penalty resolved/paid.");
                }
                return result;
            }
            return false;
        }

        public async Task<bool> DeletePenaltyAsync(int id)
        {
            var penalty = await _penaltyRepo.GetPenaltyByIdAsync(id);
            if (penalty != null)
            {

                penalty.status = "Archived";
                return await _penaltyRepo.UpdatePenaltyAsync(penalty);
            }
            return false;
        }
    }
}

