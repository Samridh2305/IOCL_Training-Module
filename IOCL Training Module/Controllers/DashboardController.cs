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

            // Fetch Employee Details
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmpNo == empNo);

            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            // Fetch Completed Trainings from CompletedTrainings table
            var completedTrainings = await _context.CompletedTrainings
                .Where(c => c.EmpNo == empNo)
                .Include(c => c.Training) // Load training details
                .ToListAsync();

            // Fetch Not Completed Trainings from NotCompletedTrainings table
            var notCompletedTrainings = await _context.NotCompletedTrainings
                .Where(nc => nc.EmpNo == empNo)
                .Include(nc => nc.Training) // Load training details
                .ToListAsync();

            // Fetch Recurring Trainings from RecurringTasks table
            var recurringTrainings = await _context.RecurringTasks
                .Where(rt => rt.EmpNo == empNo)
                .Include(rt => rt.Training) // Load training details
                .ToListAsync();

            // Populate ViewModel
            var viewModel = new DashboardViewModel
            {
                Employee = employee,
                CompletedTrainings = completedTrainings,
                NotCompletedTrainings = notCompletedTrainings,
                RecurringTrainings = recurringTrainings
            };

            return View(viewModel);
        }
    }
}
