using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IOCL_Training_Module.Data;
using IOCL_Training_Module.Models;
using System.Threading.Tasks;
using System.Linq;

namespace IOCL_Training_Module.Controllers
{
    public class CompletedController : Controller
    {
        private readonly DatabaseContext _context;

        public CompletedController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Completed
        public async Task<IActionResult> Index()
        {
            var completedTrainings = await _context.CompletedTrainings
                .Include(c => c.Employee)
                .Include(c => c.Training)
                .ToListAsync();
            return View(completedTrainings);
        }

        // GET: Completed/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var completed = await _context.CompletedTrainings
                .Include(c => c.Employee)
                .Include(c => c.Training)
                .FirstOrDefaultAsync(m => m.SrNo == id);

            if (completed == null)
                return NotFound();

            return View(completed);
        }

        // GET: Completed/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Completed/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SrNo,EmpNo,TrainingID,FromDate,ToDate")] Completed completed)
        {
            if (ModelState.IsValid)
            {
                _context.Add(completed);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(completed);
        }

        // GET: Completed/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var completed = await _context.CompletedTrainings.FindAsync(id);
            if (completed == null)
                return NotFound();

            return View(completed);
        }

        // POST: Completed/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SrNo,EmpNo,TrainingID,FromDate,ToDate")] Completed completed)
        {
            if (id != completed.SrNo)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(completed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompletedExists(completed.SrNo))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(completed);
        }

        // GET: Completed/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var completed = await _context.CompletedTrainings
                .Include(c => c.Employee)
                .Include(c => c.Training)
                .FirstOrDefaultAsync(m => m.SrNo == id);

            if (completed == null)
                return NotFound();

            return View(completed);
        }

        // POST: Completed/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var completed = await _context.CompletedTrainings.FindAsync(id);
            if (completed != null)
            {
                _context.CompletedTrainings.Remove(completed);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CompletedExists(int id)
        {
            return _context.CompletedTrainings.Any(e => e.SrNo == id);
        }
    }
}
