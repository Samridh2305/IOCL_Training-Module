using Microsoft.AspNetCore.Mvc;
using IOCL_Training_Module.Data;
using IOCL_Training_Module.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IOCL_Training_Module.Controllers
{
    public class TrainingController : Controller
    {
        private readonly DatabaseContext _context;

        public TrainingController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Trainings.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            var training = await _context.Trainings.FindAsync(id);
            if (training == null) return NotFound();
            return View(training);
        }

        public IActionResult Create() => View();

        [HttpPost]
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

        public async Task<IActionResult> Edit(string id)
        {
            var training = await _context.Trainings.FindAsync(id);
            if (training == null) return NotFound();
            return View(training);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Training training)
        {
            if (id != training.TrainingID) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(training);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(training);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var training = await _context.Trainings.FindAsync(id);
            if (training == null) return NotFound();
            return View(training);
        }

        [HttpPost, ActionName("Delete")]
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
