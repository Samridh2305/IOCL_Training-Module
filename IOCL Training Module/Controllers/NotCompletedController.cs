using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IOCL_Training_Module.Data;
using IOCL_Training_Module.Models;
using System.Threading.Tasks;
using System.Linq;

namespace IOCL_Training_Module.Controllers
{
    public class NotCompletedController : Controller
    {
        private readonly DatabaseContext _context;

        public NotCompletedController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: NotCompleted
        public async Task<IActionResult> Index()
        {
            var notCompletedList = _context.NotCompletedTrainings
                .Include(nc => nc.Employee)
                .Include(nc => nc.Training);
            return View(await notCompletedList.ToListAsync());
        }

        // GET: NotCompleted/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var notCompleted = await _context.NotCompletedTrainings
                .Include(nc => nc.Employee)
                .Include(nc => nc.Training)
                .FirstOrDefaultAsync(m => m.SNo == id);

            if (notCompleted == null) return NotFound();

            return View(notCompleted);
        }

        // GET: NotCompleted/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NotCompleted/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SNo,EmpNo,Code")] NotCompleted notCompleted)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notCompleted);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notCompleted);
        }

        // GET: NotCompleted/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var notCompleted = await _context.NotCompletedTrainings.FindAsync(id);
            if (notCompleted == null) return NotFound();

            return View(notCompleted);
        }

        // POST: NotCompleted/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SNo,EmpNo,Code")] NotCompleted notCompleted)
        {
            if (id != notCompleted.SNo) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notCompleted);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotCompletedExists(notCompleted.SNo)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(notCompleted);
        }

        // GET: NotCompleted/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var notCompleted = await _context.NotCompletedTrainings
                .Include(nc => nc.Employee)
                .Include(nc => nc.Training)
                .FirstOrDefaultAsync(m => m.SNo == id);

            if (notCompleted == null) return NotFound();

            return View(notCompleted);
        }

        // POST: NotCompleted/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notCompleted = await _context.NotCompletedTrainings.FindAsync(id);
            if (notCompleted != null)
            {
                _context.NotCompletedTrainings.Remove(notCompleted);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool NotCompletedExists(int id)
        {
            return _context.NotCompletedTrainings.Any(e => e.SNo == id);
        }
    }
}
