using Microsoft.AspNetCore.Mvc;
using IOCL_Training_Module.Data;
using IOCL_Training_Module.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

public class EmployeeController : Controller
{
    private readonly DatabaseContext _context;

    public EmployeeController(DatabaseContext context)
    {
        _context = context;
    }

    // Index (Accessible to all logged-in users)
    public IActionResult Index()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("EmpNo")))
        {
            return RedirectToAction("Login", "Account");
        }
        return View(_context.Employees.ToList());
    }
    [HttpGet]
    public IActionResult GetEmployees(string? empNo, string? name)
    {
        // 🔹 Get Logged-in Employee Number from Session
        string? loggedInEmpNo = HttpContext.Session.GetString("EmpNo");

        if (string.IsNullOrEmpty(loggedInEmpNo))
        {
            return Unauthorized(); // If session expired, return Unauthorized
        }

        // 🔹 Fetch Subordinates of the Logged-in Employee
        var subordinates = _context.Reportings
            .Where(r => r.Reporting == loggedInEmpNo)
            .Select(r => r.EmpNo)
            .ToList();

        // 🔹 Fetch Employees who are Subordinates
        var employees = _context.Employees
            .Where(e => subordinates.Contains(e.EmpNo));

        // 🔹 Apply Filters (if provided)
        if (!string.IsNullOrEmpty(empNo))
        {
            employees = employees.Where(e => e.EmpNo.Contains(empNo));
        }

        if (!string.IsNullOrEmpty(name))
        {
            employees = employees.Where(e => e.Name.Contains(name));
        }

        return Json(employees.Select(e => new { e.EmpNo, e.Name }));
    }


    // Create (Now open to all logged-in users)
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Employee employee)
    {
        if (ModelState.IsValid)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(employee);
    }

    // Edit (Now open to all logged-in users)
    public IActionResult Edit(string id)
    {
        var employee = _context.Employees.Find(id);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(string id, Employee employee)
    {
        if (id != employee.EmpNo)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            _context.Update(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(employee);
    }

    // Delete (Now open to all logged-in users)
    public IActionResult Delete(string id)
    {
        var employee = _context.Employees.Find(id);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(string EmpNo)
    {
        var employee = _context.Employees.Find(EmpNo);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
