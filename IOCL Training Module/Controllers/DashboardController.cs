using Microsoft.AspNetCore.Mvc;
using IOCL_Training_Module.Data;
using IOCL_Training_Module.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IOCL_Training_Module.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DatabaseContext _context;

        public DashboardController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string empNo)
        {
            if (string.IsNullOrEmpty(empNo))
            {
                return View(null);
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmpNo == empNo);

            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            var completedTrainings = await _context.CompletedTrainings
                .Where(c => c.EmpNo == empNo)
                .Include(c => c.Training)
                .ToListAsync();

            var notCompletedTrainings = await _context.NotCompletedTrainings
                .Where(nc => nc.EmpNo == empNo)
                .Include(nc => nc.Training)
                .ToListAsync();

            var recurringTrainings = await _context.RecurringTasks
                .Where(rt => rt.EmpNo == empNo)
                .Include(rt => rt.Training)
                .ToListAsync();

            var viewModel = new DashboardViewModel
            {
                Employee = employee,
                CompletedTrainings = completedTrainings,
                NotCompletedTrainings = notCompletedTrainings,
                RecurringTrainings = recurringTrainings
            };

            return View(viewModel);
        }

        public async Task<IActionResult> NotCompleted(string empNo)
        {
            if (string.IsNullOrEmpty(empNo))
            {
                return RedirectToAction("Index");
            }

            var notCompletedTrainings = await _context.NotCompletedTrainings
                .Where(nc => nc.EmpNo == empNo)
                .Include(nc => nc.Training)
                .ToListAsync();

            return View(notCompletedTrainings);
        }
    }
}
