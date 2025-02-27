using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IOCL_Training_Module.Data;
using IOCL_Training_Module.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IOCL_Training_Module.Controllers
{
    public class RecurringTrainingController : Controller
    {
        private readonly DatabaseContext _context;

        public RecurringTrainingController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: RecurringTraining
        public async Task<IActionResult> Index()
        {
            var recurringList = _context.RecurringTasks
                .Include(rt => rt.Employee)
                .Include(rt => rt.Training);
            return View(await recurringList.ToListAsync());
        }

        // GET: RecurringTraining/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var recurringTraining = await _context.RecurringTasks
                .Include(rt => rt.Employee)
                .Include(rt => rt.Training)
                .FirstOrDefaultAsync(m => m.SrNo == id);

            return recurringTraining == null ? NotFound() : View(recurringTraining);
        }

        // GET: RecurringTraining/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RecurringTraining/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SrNo,EmpNo,TrainingID,FromDate,ToDate")] RecurringTraining recurringTraining)
        {
            if (!ModelState.IsValid) return View(recurringTraining);

            // No need to calculate NextTrainingDate in C# as SQL trigger handles it
            _context.Add(recurringTraining);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: RecurringTraining/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var recurringTraining = await _context.RecurringTasks.FindAsync(id);
            return recurringTraining == null ? NotFound() : View(recurringTraining);
        }

        // POST: RecurringTraining/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SrNo,EmpNo,TrainingID,FromDate,ToDate")] RecurringTraining recurringTraining)
        {
            if (id != recurringTraining.SrNo) return NotFound();

            if (!ModelState.IsValid) return View(recurringTraining);

            try
            {
                _context.Update(recurringTraining);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecurringTrainingExists(recurringTraining.SrNo)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: RecurringTraining/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var recurringTraining = await _context.RecurringTasks
                .Include(rt => rt.Employee)
                .Include(rt => rt.Training)
                .FirstOrDefaultAsync(m => m.SrNo == id);

            return recurringTraining == null ? NotFound() : View(recurringTraining);
        }

        // POST: RecurringTraining/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recurringTraining = await _context.RecurringTasks.FindAsync(id);
            if (recurringTraining != null)
            {
                _context.RecurringTasks.Remove(recurringTraining);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool RecurringTrainingExists(int id)
        {
            return _context.RecurringTasks.Any(e => e.SrNo == id);
        }

        // API to Fetch Upcoming Recurring Trainings in JSON Format
        [HttpGet]
        public async Task<IActionResult> GetRecurringTrainings()
        {
            var recurringTrainings = await _context.RecurringTasks
                .Include(rt => rt.Employee)
                .Include(rt => rt.Training)
                .ToListAsync();

            if (!recurringTrainings.Any())
                return Json(new { message = "No recurring trainings found in the database." });

            var result = recurringTrainings
                .Where(rt => rt.NextTrainingDate >= DateTime.Today)
                .Select(rt => new
                {
                    TrainingName = rt.Training?.TrainingName ?? "Unknown Training",
                    Venue = rt.Training?.Venue ?? "Unknown Venue",
                    NextTrainingDate = rt.NextTrainingDate != default ? rt.NextTrainingDate.ToString("yyyy-MM-dd") : "Not Scheduled"
                }).ToList();

            return result.Any() ? Json(result) : Json(new { message = "No upcoming trainings found." });
        }
    }
}
