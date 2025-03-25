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
