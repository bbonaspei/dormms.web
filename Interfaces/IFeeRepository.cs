using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IFeeRepository
    {
        Task<IEnumerable<Fee>> GetAllFeesAsync();
        Task<Fee?> GetFeeByIdAsync(int id);
        Task<bool> AddFeeAsync(Fee fee);
        Task<bool> UpdateFeeAsync(Fee fee);
        Task<bool> DeleteFeeAsync(int id);
    }
}

