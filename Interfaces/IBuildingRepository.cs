using Microsoft.AspNetCore.Mvc;
using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IBuildingRepository 
    {
       Task<IEnumerable<Building>> GetAllAsync();
       Task<Building?> GetByIdAsync(int id);
       Task AddAsync(Building building);
       Task UpdateAsync(Building building);

       Task DeleteAsync(int id);
       Task<bool> IsCodeUniqueAsync(string code, int? excludeId = null);
    }
}

