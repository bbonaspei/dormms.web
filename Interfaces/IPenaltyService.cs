using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IPenaltyService
    {
        Task<IEnumerable<Penalty>> GetPenaltiesAsync();
        Task<IEnumerable<Penalty>> GetStudentPenaltiesAsync(int studentId);
        Task<Penalty?> GetPenaltyByIdAsync(int id);
        Task<bool> ApplyPenaltyAsync(Penalty penalty);
        Task<bool> ResolvePenaltyAsync(int penaltyId);
        Task<bool> DeletePenaltyAsync(int id);
    }
}

