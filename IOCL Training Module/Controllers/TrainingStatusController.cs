using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IOCL_Training_Module.Data;
using IOCL_Training_Module.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

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
        public async Task<IActionResult> Index(
            string? selectedEmpNo, string? trainingName, string? venue, string? type,
            string? department, string? designation, string? sex,
            string? status, string? safetyTraining, DateTime? fromDate, DateTime? toDate)
        {
            string loggedInEmpNo = HttpContext.Session.GetString("EmpNo") ?? "";

            // Fetch subordinates from the Reportings table
            var subordinates = await _context.Reportings
                .Where(r => r.Reporting == loggedInEmpNo)
                .Select(r => r.EmpNo)
                .ToListAsync();

            // If no subordinates exist, redirect
            if (!subordinates.Any())
            {
                TempData["NotAuthorized"] = "You are not authorized to view this page.";
                return RedirectToAction("Index", "Dashboard");
            }

            // Fetch all completed trainings of subordinates, joined with Trainings & Employees table
            var completedTrainingsQuery = _context.CompletedTrainings
                .Include(c => c.Employee) // Join with Employee table
                .Include(c => c.Training) // Join with Trainings table
                .Where(c => subordinates.Contains(c.EmpNo)) // Fetch trainings of subordinates
                .AsQueryable();

            // 🔹 Apply Employee-related Filters
            if (!string.IsNullOrEmpty(selectedEmpNo) && subordinates.Contains(selectedEmpNo))
                completedTrainingsQuery = completedTrainingsQuery.Where(c => c.EmpNo == selectedEmpNo);

            if (!string.IsNullOrEmpty(designation))
                completedTrainingsQuery = completedTrainingsQuery.Where(c => c.Employee!.EmpDesignation == designation);

            if (!string.IsNullOrEmpty(sex))
                completedTrainingsQuery = completedTrainingsQuery.Where(c => c.Employee!.Sex == sex);

            // 🔹 Apply Training-related Filters
            if (!string.IsNullOrEmpty(trainingName))
                completedTrainingsQuery = completedTrainingsQuery.Where(c => c.Training!.TrainingName.Contains(trainingName));

            if (!string.IsNullOrEmpty(venue))
                completedTrainingsQuery = completedTrainingsQuery.Where(c => c.Training!.Venue.Contains(venue));

            if (!string.IsNullOrEmpty(type))
                completedTrainingsQuery = completedTrainingsQuery.Where(c => c.Training!.Type == type);

            if (!string.IsNullOrEmpty(department))
                completedTrainingsQuery = completedTrainingsQuery.Where(c => c.Training!.Department == department);

            if (!string.IsNullOrEmpty(status))
                completedTrainingsQuery = completedTrainingsQuery.Where(c => c.Training!.Status == status);

            if (!string.IsNullOrEmpty(safetyTraining))
                completedTrainingsQuery = completedTrainingsQuery.Where(c => c.Training!.SafetyTraining == safetyTraining);

            if (fromDate.HasValue)
                completedTrainingsQuery = completedTrainingsQuery.Where(c => c.FromDate >= fromDate.Value);

            if (toDate.HasValue)
                completedTrainingsQuery = completedTrainingsQuery.Where(c => c.ToDate <= toDate.Value);

            // Fetch NotCompleted and Upcoming Trainings without filters
            var notCompletedTrainings = await _context.NotCompletedTrainings
                .Include(nc => nc.Employee)
                .Include(nc => nc.Training)
                .Where(nc => subordinates.Contains(nc.EmpNo))
                .ToListAsync();

            var upcomingTrainings = await _context.RecurringTasks
                .Include(ut => ut.Training)
                .Where(ut => subordinates.Contains(ut.EmpNo))
                .ToListAsync();

            // 🔹 Fetch distinct dropdown options for filters
            var trainingTypes = await _context.Trainings.Select(t => t.Type).Distinct().ToListAsync();
            var departments = await _context.Trainings.Select(t => t.Department).Distinct().ToListAsync();
            var statuses = await _context.Trainings.Select(t => t.Status).Distinct().ToListAsync();
            var designations = await _context.Employees.Select(e => e.EmpDesignation).Distinct().ToListAsync();

            // Assign data to ViewBag for dropdowns
            ViewBag.TrainingTypes = trainingTypes.Select(t => new SelectListItem { Value = t, Text = t }).ToList();
            ViewBag.Departments = departments.Select(d => new SelectListItem { Value = d, Text = d }).ToList();
            ViewBag.Statuses = statuses.Select(s => new SelectListItem { Value = s, Text = s }).ToList();
            ViewBag.Designations = designations.Select(d => new SelectListItem { Value = d, Text = d }).ToList();
            ViewBag.SexOptions = new List<SelectListItem>
    {
        new SelectListItem { Value = "M", Text = "Male" },
        new SelectListItem { Value = "F", Text = "Female" }
    };
            ViewBag.SafetyTrainings = new List<SelectListItem>
    {
        new SelectListItem { Value = "Yes", Text = "Yes" },
        new SelectListItem { Value = "No", Text = "No" }
    };

            // Populate Subordinates List
            var subordinateEmployees = await _context.Employees
                .Where(e => subordinates.Contains(e.EmpNo))
                .ToListAsync();

            var viewModel = new TrainingStatusViewModel
            {
                CompletedTrainings = await completedTrainingsQuery.ToListAsync(),
                NotCompletedTrainings = notCompletedTrainings,
                UpcomingTrainings = upcomingTrainings,
                Subordinates = subordinateEmployees,
                SelectedEmpNo = selectedEmpNo,
                Designation = designation,
                Sex = sex
            };

            return View(viewModel);
        }

    }
}
