using Microsoft.AspNetCore.Mvc;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;

namespace DormMS.Web.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository _repo;
        private readonly IAuditService _audit;

        public BuildingService(IBuildingRepository repo, IAuditService audit)
        {
            _repo = repo;
            _audit = audit;
        }

        public async Task<IEnumerable<Building>> GetBuildingsListAsync() => await _repo.GetAllAsync();
        public async Task CreateBuildingAsync(Building building)
        {

            if (string.IsNullOrEmpty(building.status))
            {
                building.status = "Active";
            }

            if (!await _repo.IsCodeUniqueAsync(building.buildingCode))
            {
                throw new InvalidOperationException($"Building code '{building.buildingCode}' is already in use by another building.");
            }

            await _repo.AddAsync(building);

            await _audit.LogActionAsync(
                "CREATE",
                "Building",
                building.id,
                null,
                building.buildingName
            );
        }
        public async Task<Building?> GetBuildingByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task UpdateBuildingAsync(Building building)
        {

            if (!await _repo.IsCodeUniqueAsync(building.buildingCode, building.id))
            {
                throw new InvalidOperationException($"Building code '{building.buildingCode}' is already assigned to another block.");
            }

            await _repo.UpdateAsync(building);
        }

        public async Task DeleteBuildingAsync(int id)
        {
            var building = await _repo.GetByIdAsync(id);
            if (building != null)
            {
                await _repo.DeleteAsync(id);

                await _audit.LogActionAsync(
                    "DELETE",
                    "Building",
                    id,
                    building.buildingName,
                    null
                );
            }
        }
    }
}

