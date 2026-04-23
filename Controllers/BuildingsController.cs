using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormMS.Web.Controllers
{
    [Authorize(Roles = "Admin,DormManager")]
    [Route("Buildings/[action]")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class BuildingsController : Controller
    {
        private readonly IBuildingService _buildingService;
        private readonly IAuditService _audit;

        public BuildingsController(IBuildingService buildingService, IAuditService audit)
        {
            _buildingService = buildingService;
            _audit = audit;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var buildings = await _buildingService.GetBuildingsListAsync();
            return View(buildings);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Building building)
        {
            if (ModelState.IsValid)
            {
                try 
                {

                    await _buildingService.CreateBuildingAsync(building);

                    await _audit.LogActionAsync("CREATE", "Building", building.id, null, $"Building {building.buildingName} added to infrastructure.");

                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("buildingCode", ex.Message);
                }
            }
            return View(building);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var building = await _buildingService.GetBuildingByIdAsync(id.Value);
            if (building == null) return NotFound();

            return View(building);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Building building)
        {
            if (id != building.id) return NotFound();

            if (ModelState.IsValid)
            {
                try 
                {
                    await _buildingService.UpdateBuildingAsync(building);

                    await _audit.LogActionAsync("UPDATE", "Building", building.id, null, $"Building {building.buildingName} updated.");

                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("buildingCode", ex.Message);
                }
            }
            return View(building);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _buildingService.DeleteBuildingAsync(id);
                TempData["Success"] = "Building removed successfully.";
            }
            catch (Exception)
            {
                TempData["Error"] = "Unable to delete building. Ensure it has no linked rooms or active dependencies.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

