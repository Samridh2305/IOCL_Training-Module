using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IOCL_Training_Module.Data;
using IOCL_Training_Module.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace IOCL_Training_Module.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DatabaseContext _context;

        public DashboardController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? selectedEmpNo = null)
        {
            string? empNo = HttpContext.Session.GetString("EmpNo");

            if (string.IsNullOrEmpty(empNo))
            {
                return RedirectToAction("Login", "Home"); // Redirect to login if not authenticated
            }

            // Get the logged-in employee
            var loggedInEmployee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmpNo == empNo);

            if (loggedInEmployee == null)
            {
                return NotFound("Employee not found.");
            }

            // Fetch employees who report to the logged-in user
            var reportingEmployees = await _context.Reportings
                .Where(r => r.Reporting == empNo)
                .Select(r => r.Employee)
                .ToListAsync();

            // Default to logged-in employee if no subordinate is selected
            if (string.IsNullOrEmpty(selectedEmpNo))
            {
                selectedEmpNo = empNo;
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmpNo == selectedEmpNo);

            if (employee == null)
            {
                return NotFound("Selected employee not found.");
            }

            // Get the reporting manager of the selected employee
            var reportingManager = await _context.Reportings
                .Where(r => r.EmpNo == selectedEmpNo)
                .Select(r => r.Manager)
                .FirstOrDefaultAsync();

            // Fetch training details
            var completedTrainings = await _context.CompletedTrainings
                .Where(c => c.EmpNo == selectedEmpNo)
                .Include(c => c.Training)
                .ToListAsync();

            var notCompletedTrainings = await _context.NotCompletedTrainings
                .Where(nc => nc.EmpNo == selectedEmpNo)
                .Include(nc => nc.Training)
                .ToListAsync();

            var recurringTrainings = await _context.RecurringTasks
                .Where(rt => rt.EmpNo == selectedEmpNo)
                .Include(rt => rt.Training)
                .ToListAsync();

            var viewModel = new DashboardViewModel
            {
                Employee = employee, // The currently selected employee
                LoggedInEmployee = loggedInEmployee, // Preserve logged-in user
                ReportingEmployees = reportingEmployees,
                ReportingManager = reportingManager,
                CompletedTrainings = completedTrainings,
                NotCompletedTrainings = notCompletedTrainings,
                RecurringTrainings = recurringTrainings
            };

            return View(viewModel);
        }

        public async Task<IActionResult> NotCompleted(string? selectedEmpNo = null)
        {
            string? loggedInEmpNo = HttpContext.Session.GetString("EmpNo");

            if (string.IsNullOrEmpty(loggedInEmpNo))
            {
                return RedirectToAction("Login", "Home");
            }

            if (string.IsNullOrEmpty(selectedEmpNo))
            {
                selectedEmpNo = loggedInEmpNo; // Default to logged-in employee
            }

            var notCompletedTrainings = await _context.NotCompletedTrainings
                .Where(nc => nc.EmpNo == selectedEmpNo)
                .Include(nc => nc.Training)
                .ToListAsync();

            ViewBag.SelectedEmpNo = selectedEmpNo; // Pass employee number to the view

            return View(notCompletedTrainings);
        }


        public async Task<IActionResult> Completed(string? selectedEmpNo = null)
        {
            string? loggedInEmpNo = HttpContext.Session.GetString("EmpNo");

            if (string.IsNullOrEmpty(loggedInEmpNo))
            {
                return RedirectToAction("Login", "Home");
            }

            if (string.IsNullOrEmpty(selectedEmpNo))
            {
                selectedEmpNo = loggedInEmpNo; // Default to logged-in employee
            }

            var completedTrainings = await _context.CompletedTrainings
                .Where(c => c.EmpNo == selectedEmpNo)
                .Include(c => c.Training)
                .ToListAsync();

            ViewBag.SelectedEmpNo = selectedEmpNo; // Pass employee number to the view

            return View(completedTrainings);
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }
    }
}
