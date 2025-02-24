using Microsoft.AspNetCore.Mvc;
using IOCL_Training_Module.Data; // Database context
using IOCL_Training_Module.Models; // Employee model
using Microsoft.AspNetCore.Http; // For session handling
using System.Linq; // For LINQ queries

public class AccountController : Controller
{
    private readonly DatabaseContext _context;

    public AccountController(DatabaseContext context)
    {
        _context = context;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string EmpNo, string Password)
    {
        var employee = _context.Employees.FirstOrDefault(e => e.EmpNo == EmpNo);

        if (employee != null)
        {
            if (employee.Password == Password) // Plaintext check (Consider Hashing)
            {
                HttpContext.Session.SetString("EmpNo", EmpNo);
                return RedirectToAction("Index", "Dashboard", new { empNo = EmpNo });
            }
            else
            {
                ViewBag.Error = "Invalid Password. Please try again.";
            }
        }
        else
        {
            ViewBag.Error = "Invalid Employee Number. Please try again.";
        }

        return View();
    }
}
