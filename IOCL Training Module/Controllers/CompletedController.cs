using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IOCL_Training_Module.Data;
using IOCL_Training_Module.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; // Added for session handling
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace IOCL_Training_Module.Controllers
{
    public class CompletedController : Controller
    {
        private readonly DatabaseContext _context;

        public CompletedController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Completed Trainings
        public async Task<IActionResult> Index(
            string trainingName, string venue, string type, string department, string status,
            string safetyTraining, DateTime? fromDate, DateTime? toDate)
        {
            // 🔹 Get the currently logged-in employee's EmpNo from session
            var empNo = HttpContext.Session.GetString("EmpNo");
            if (string.IsNullOrEmpty(empNo))
            {
                Console.WriteLine("User is not authenticated.");
                return Unauthorized(); // Ensure user is logged in
            }
            Console.WriteLine($"Logged in user: {empNo}");

            // Fetch completed trainings for the logged-in employee
            var completedTrainings = _context.CompletedTrainings
                .Include(c => c.Training)  // Include Training details
                .Where(c => c.EmpNo == empNo)  // Filter for the logged-in employee
                .AsQueryable();

            // Apply filters if provided
            if (!string.IsNullOrEmpty(trainingName))
                completedTrainings = completedTrainings.Where(c => c.Training!.TrainingName.Contains(trainingName));

            if (!string.IsNullOrEmpty(venue))
                completedTrainings = completedTrainings.Where(c => c.Training!.Venue.Contains(venue));

            if (!string.IsNullOrEmpty(type))
                completedTrainings = completedTrainings.Where(c => c.Training!.Type == type);

            if (!string.IsNullOrEmpty(department))
                completedTrainings = completedTrainings.Where(c => c.Training!.Department == department);

            if (!string.IsNullOrEmpty(status))
                completedTrainings = completedTrainings.Where(c => c.Training!.Status == status);

            if (!string.IsNullOrEmpty(safetyTraining))
                completedTrainings = completedTrainings.Where(c => c.Training!.SafetyTraining == safetyTraining);

            if (fromDate.HasValue)
                completedTrainings = completedTrainings.Where(c => c.FromDate >= fromDate.Value);

            if (toDate.HasValue)
                completedTrainings = completedTrainings.Where(c => c.ToDate <= toDate.Value);

            // Fetch distinct dropdown options
            var trainingTypes = await _context.Trainings.Select(t => t.Type).Distinct().ToListAsync();
            var departments = await _context.Trainings.Select(t => t.Department).Distinct().ToListAsync();
            var statuses = await _context.Trainings.Select(t => t.Status).Distinct().ToListAsync();

            // Assign data to ViewBag as List<SelectListItem>
            ViewBag.TrainingTypes = trainingTypes.Select(t => new SelectListItem { Value = t, Text = t }).ToList();
            ViewBag.Departments = departments.Select(d => new SelectListItem { Value = d, Text = d }).ToList();
            ViewBag.Statuses = statuses.Select(s => new SelectListItem { Value = s, Text = s }).ToList();
            ViewBag.SafetyTrainings = new List<SelectListItem>
            {
                new SelectListItem { Value = "Yes", Text = "Yes" },
                new SelectListItem { Value = "No", Text = "No" }
            };

            return View(await completedTrainings.ToListAsync());
        }
    }
}