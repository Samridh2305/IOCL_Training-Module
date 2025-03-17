using Microsoft.AspNetCore.Mvc;
using IOCL_Training_Module.Data;
using IOCL_Training_Module.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IOCL_Training_Module.Controllers
{
    [Route("Training")]
    public class TrainingController : Controller
    {
        private readonly DatabaseContext _context;

        public TrainingController(DatabaseContext context)
        {
            _context = context;
        }

        // Index (List all trainings)
        [HttpGet("")]
        public async Task<IActionResult> Index(string trainingName, string venue, DateTime? fromDate, DateTime? toDate, string type)
        {
            var trainings = _context.Trainings.AsQueryable();

            if (!string.IsNullOrEmpty(trainingName))
                trainings = trainings.Where(t => t.TrainingName.Contains(trainingName));

            if (!string.IsNullOrEmpty(venue))
                trainings = trainings.Where(t => t.Venue.Contains(venue));

            if (fromDate.HasValue)
                trainings = trainings.Where(t => t.FromDate >= fromDate.Value);

            if (toDate.HasValue)
                trainings = trainings.Where(t => t.ToDate <= toDate.Value);

            if (!string.IsNullOrEmpty(type))
                trainings = trainings.Where(t => t.Type == type);

            ViewBag.TrainingTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "-- Select Type --" },
                new SelectListItem { Value = "General Awareness", Text = "General Awareness" },
                new SelectListItem { Value = "Functional", Text = "Functional" },
                new SelectListItem { Value = "Developmental", Text = "Developmental" }
            };

            return View(await trainings.ToListAsync());
        }

        // Training details
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var training = await _context.Trainings.FindAsync(id);
            if (training == null) return NotFound();
            return View(training);
        }

        // Training statistics (JSON response)
        [HttpGet("TrainingStatistics")]
        public async Task<IActionResult> TrainingStatistics()
        {
            var generalAwarenessCount = await _context.Trainings.CountAsync(t => t.Type == "General Awareness");
            var functionalCount = await _context.Trainings.CountAsync(t => t.Type == "Functional");
            var developmentalCount = await _context.Trainings.CountAsync(t => t.Type == "Developmental");

            var data = new
            {
                GeneralAwareness = generalAwarenessCount,
                Functional = functionalCount,
                Developmental = developmentalCount
            };

            return Json(data);
        }

        [HttpGet("CategoryCompletion")]
        public async Task<IActionResult> CategoryCompletion()
        {
            var categoryCompletion = await _context.Trainings
                .GroupBy(t => t.Type)
                .Select(group => new
                {
                    Category = group.Key,
                    Completed = _context.CompletedTrainings
                        .Where(ct => ct.Training != null && ct.Training.Type == group.Key)
                        .Count(),
                    Pending = _context.NotCompletedTrainings
                        .Where(nct => nct.Training != null && nct.Training.Type == group.Key)
                        .Count()
                })
                .ToDictionaryAsync(t => t.Category);

            return Json(categoryCompletion);
        }

        // Create Training - Show form
        [HttpGet("Create")]
        public IActionResult Create() => View();

        // Create Training - Submit form
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Training training)
        {
            if (ModelState.IsValid)
            {
                _context.Add(training);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(training);
        }

        // Edit Training - Show form
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var training = await _context.Trainings.FindAsync(id);
            if (training == null) return NotFound();
            return View(training);
        }

        // Edit Training - Submit form
        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, [Bind("TrainingID,TrainingName,Duration,Venue,Department,Validity,FromDate,ToDate,FPR,Status,Type,SafetyTraining,FacultyName")] Training training)
        {
            if (id != training.TrainingID) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(training);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Trainings.Any(e => e.TrainingID == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(training);
        }

        // Delete Training - Show confirmation page
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var training = await _context.Trainings.FindAsync(id);
            if (training == null) return NotFound();
            return View(training);
        }

        // Delete Training - Confirm delete
        [HttpPost("DeleteConfirmed/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var training = await _context.Trainings.FindAsync(id);
            if (training != null)
            {
                _context.Trainings.Remove(training);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
