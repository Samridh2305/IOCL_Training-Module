using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IOCL_Training_Module.Data;
using IOCL_Training_Module.Models;

namespace IOCL_Training_Module.Controllers
{
    public class SubordinateTrainingController : Controller
    {
        private readonly DatabaseContext _context;

        public SubordinateTrainingController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: SubordinateTraining/Completed
        public async Task<IActionResult> Completed(
    string? selectedEmpNo, string trainingName, string venue, string type,
    string department, string status, string safetyTraining,
    DateTime? fromDate, DateTime? toDate)
        {
            // Get the logged-in employee number
            string? loggedInEmpNo = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(loggedInEmpNo))
            {
                return Unauthorized(); // Ensure user is logged in
            }

            // ✅ Fetch subordinates (employees who report to the logged-in employee)
            var subordinates = await _context.Reportings
                .Where(r => r.Reporting == loggedInEmpNo) // Only subordinates of the logged-in user
                .Select(r => r.EmpNo) // Get the subordinate's EmpNo
                .ToListAsync();

            if (!subordinates.Any())
            {
                // Log warning if no subordinates found
                Console.WriteLine($"No subordinates found for manager {loggedInEmpNo}");
            }

            // ✅ Fetch subordinate employee details for the dropdown
            var subordinateEmployees = await _context.Employees
                .Where(e => subordinates.Contains(e.EmpNo))
                .ToListAsync();

            // ✅ Initialize the query (return empty list if no subordinate is selected)
            var completedTrainings = _context.CompletedTrainings
                .Include(ct => ct.Training)
                .Where(ct => !string.IsNullOrEmpty(selectedEmpNo) && ct.EmpNo == selectedEmpNo)
                .AsQueryable();

            // ✅ Apply filters
            if (!string.IsNullOrEmpty(trainingName))
                completedTrainings = completedTrainings.Where(ct => ct.Training!.TrainingName.Contains(trainingName));

            if (!string.IsNullOrEmpty(venue))
                completedTrainings = completedTrainings.Where(ct => ct.Training!.Venue.Contains(venue));

            if (!string.IsNullOrEmpty(type))
                completedTrainings = completedTrainings.Where(ct => ct.Training!.Type == type);

            if (!string.IsNullOrEmpty(department))
                completedTrainings = completedTrainings.Where(ct => ct.Training!.Department == department);

            if (!string.IsNullOrEmpty(status))
                completedTrainings = completedTrainings.Where(ct => ct.Training!.Status == status);

            if (!string.IsNullOrEmpty(safetyTraining))
                completedTrainings = completedTrainings.Where(ct => ct.Training!.SafetyTraining == safetyTraining);

            if (fromDate.HasValue)
                completedTrainings = completedTrainings.Where(ct => ct.FromDate >= fromDate.Value);

            if (toDate.HasValue)
                completedTrainings = completedTrainings.Where(ct => ct.ToDate <= toDate.Value);

            // ✅ Fetch distinct dropdown options
            var trainingTypes = await _context.Trainings.Select(t => t.Type).Distinct().ToListAsync();
            var departments = await _context.Trainings.Select(t => t.Department).Distinct().ToListAsync();
            var statuses = await _context.Trainings.Select(t => t.Status).Distinct().ToListAsync();

            // ✅ Assign data to ViewBag
            ViewBag.TrainingTypes = trainingTypes.Select(t => new SelectListItem { Value = t, Text = t }).ToList();
            ViewBag.Departments = departments.Select(d => new SelectListItem { Value = d, Text = d }).ToList();
            ViewBag.Statuses = statuses.Select(s => new SelectListItem { Value = s, Text = s }).ToList();
            ViewBag.SafetyTrainings = new List<SelectListItem>
    {
        new SelectListItem { Value = "Yes", Text = "Yes" },
        new SelectListItem { Value = "No", Text = "No" }
    };

            var viewModel = new SubordinateTrainingViewModel
            {
                CompletedTrainings = await completedTrainings.ToListAsync(),
                Subordinates = subordinateEmployees,
                SelectedEmpNo = selectedEmpNo
            };

            return View(viewModel);
        }

    }
}
