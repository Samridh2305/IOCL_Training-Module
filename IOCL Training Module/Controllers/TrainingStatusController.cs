using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IOCL_Training_Module.Data;
using IOCL_Training_Module.Models;
using System.Threading.Tasks;
using System.Linq;

namespace IOCL_Training_Module.Controllers
{
    public class TrainingStatusController : Controller
    {
        private readonly DatabaseContext _context;

        public TrainingStatusController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: TrainingStatus
        public async Task<IActionResult> Index(string? selectedEmpNo)
        {
            string loggedInEmpNo = HttpContext.Session.GetString("EmpNo") ?? "";

            // Fetch subordinates from the Reportings table
            var subordinates = await _context.Reportings
                .Where(r => r.Reporting == loggedInEmpNo)
                .Select(r => r.EmpNo)
                .ToListAsync();

            // If no subordinates exist, show a pop-up and redirect
            if (!subordinates.Any())
            {
                TempData["NotAuthorized"] = "You are not authorized to view this page.";
                return RedirectToAction("Index", "Dashboard");
            }

            // If no employee is selected, default to the first subordinate
            if (string.IsNullOrEmpty(selectedEmpNo) || !subordinates.Contains(selectedEmpNo))
            {
                selectedEmpNo = subordinates.First();
            }

            var completedTrainings = await _context.CompletedTrainings
                .Include(c => c.Employee)
                .Include(c => c.Training)
                .Where(c => c.EmpNo == selectedEmpNo)
                .ToListAsync();

            var notCompletedTrainings = await _context.NotCompletedTrainings
                .Include(nc => nc.Employee)
                .Include(nc => nc.Training)
                .Where(nc => nc.EmpNo == selectedEmpNo)
                .ToListAsync();

            var upcomingTrainings = await _context.RecurringTasks
                .Include(ut => ut.Training)
                .Where(ut => ut.EmpNo == selectedEmpNo)
                .ToListAsync();

            var viewModel = new TrainingStatusViewModel
            {
                CompletedTrainings = completedTrainings,
                NotCompletedTrainings = notCompletedTrainings,
                UpcomingTrainings = upcomingTrainings, // Ensure UpcomingTrainings is set
                Subordinates = await _context.Employees
                    .Where(e => subordinates.Contains(e.EmpNo))
                    .ToListAsync(),
                SelectedEmpNo = selectedEmpNo
            };

            return View(viewModel);
        }
    }
}