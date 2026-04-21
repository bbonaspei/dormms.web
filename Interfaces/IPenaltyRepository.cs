using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IPenaltyRepository
    {
        Task<IEnumerable<Penalty>> GetAllPenaltiesAsync();
        Task<Penalty?> GetPenaltyByIdAsync(int id);
        Task<bool> AddPenaltyAsync(Penalty penalty);
        Task<bool> UpdatePenaltyAsync(Penalty penalty);
        Task<IEnumerable<Penalty>> GetByStudentIdAsync(int studentId);
    }
}
