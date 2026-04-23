using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DormMS.Web.Data;

namespace DormMS.Web.Controllers
{
    [Authorize(Roles = "Admin,Manager,DormManager,Finance")]
    [Route("Penalties/[action]/{id?}")]
    public class PenaltiesController : Controller
    {
        private readonly IPenaltyService _penaltyService;
        private readonly IStudentService _studentService;
        private readonly ApplicationDbContext _context;

        public PenaltiesController(IPenaltyService penaltyService, IStudentService studentService, ApplicationDbContext context)
        {
            _penaltyService = penaltyService;
            _studentService = studentService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var penalties = await _penaltyService.GetPenaltiesAsync();
            return View(penalties.Where(p => p.status != "Archived"));
        }

        [HttpGet]
        public async Task<IActionResult> Archive()
        {
            var penalties = await _penaltyService.GetPenaltiesAsync();
            return View(penalties.Where(p => p.status == "Archived"));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? studentId)
        {
            var students = await _studentService.GetStudentListAsync();
            ViewBag.Students = new SelectList(students.Select(s => new { 
                id = s.id, 
                displayText = $"{s.User?.firstName} {s.User?.lastName} ({s.studentId})" 
            }), "id", "displayText", studentId);

            return View(new Penalty { studentId = studentId ?? 0 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Penalty penalty)
        {
            if (ModelState.IsValid)
            {
                await _penaltyService.ApplyPenaltyAsync(penalty);
                TempData["Success"] = "Penalty applied and notification sent to the student.";
                return RedirectToAction(nameof(Index));
            }
            
            var students = await _studentService.GetStudentListAsync();
            ViewBag.Students = new SelectList(students.Select(s => new { 
                id = s.id, 
                displayText = $"{s.User?.firstName} {s.User?.lastName} ({s.studentId})" 
            }), "id", "displayText", penalty.studentId);
            
            return View(penalty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Resolve(int id)
        {
            var result = await _penaltyService.ResolvePenaltyAsync(id);
            if (result)
            {
                TempData["Success"] = "Penalty marked as resolved.";
            }
            else
            {
                TempData["Error"] = "Failed to resolve penalty.";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _penaltyService.DeletePenaltyAsync(id);
            if (result)
            {
                TempData["Success"] = "Penalty archived.";
            }
            else
            {
                TempData["Error"] = "Failed to archive penalty.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

