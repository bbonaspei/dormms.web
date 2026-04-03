using Microsoft.AspNetCore.Mvc;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;

namespace DormMS.Web.Controllers
{
    public class BuildingsController : Controller
    {
        private readonly IBuildingService _buildingService;
        public BuildingsController(IBuildingService buildingService) { _buildingService = buildingService; }

        public async Task<IActionResult> Index()
        {
            var buildings = await _buildingService.GetBuildingsListAsync();
            return View(buildings);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Building building)
        {
            if(ModelState.IsValid)
            {
                await _buildingService.CreateBuildingAsync(building);
                return RedirectToAction(nameof(Index));
            }
            return View(building);
        }
    }
}
