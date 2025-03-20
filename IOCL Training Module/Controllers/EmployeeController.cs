using Microsoft.AspNetCore.Mvc;
using IOCL_Training_Module.Data;
using IOCL_Training_Module.Models;
using Microsoft.AspNetCore.Http;

public class EmployeeController : Controller
{
    private readonly DatabaseContext _context;

    public EmployeeController(DatabaseContext context)
    {
        _context = context;
    }

    // Helper method to check if user is admin
    private bool IsAdmin()
    {
        var role = HttpContext.Session.GetString("Role");
        return role == "Admin";
    }

    // Index (accessible to all logged-in users)
    public IActionResult Index()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("EmpNo")))
        {
            return RedirectToAction("Login", "Account");
        }
        return View(_context.Employees.ToList());
    }

    // Create (Admin only)
    public IActionResult Create()
    {
        if (!IsAdmin())
        {
            return Unauthorized("Only Admins can create employees.");
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Employee employee)
    {
        if (!IsAdmin())
        {
            return Unauthorized("Only Admins can create employees.");
        }

        if (ModelState.IsValid)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(employee);
    }

    // Edit (Admin only)
    public IActionResult Edit(string id)
    {
        if (!IsAdmin())
        {
            return Unauthorized("Only Admins can edit employees.");
        }

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
        if (!IsAdmin())
        {
            return Unauthorized("Only Admins can edit employees.");
        }

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

    // Delete (Admin only)
    public IActionResult Delete(string id)
    {
        if (!IsAdmin())
        {
            return Unauthorized("Only Admins can delete employees.");
        }

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
        if (!IsAdmin())
        {
            return Unauthorized("Only Admins can delete employees.");
        }

        var employee = _context.Employees.Find(EmpNo);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}