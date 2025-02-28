using Microsoft.AspNetCore.Mvc;
using IOCL_Training_Module.Data; // Database context
using IOCL_Training_Module.Models; // Employee model
using Microsoft.AspNetCore.Http; // For session handling
using System.Linq; // For LINQ queries
using System.Threading.Tasks;

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
            if (employee.Password == Password) // Directly compare stored password
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

    // GET: Forgot Password Page
    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    // POST: Reset Password
    [HttpPost]
    public async Task<IActionResult> ForgotPassword(string EmpNo, string NewPassword, string ConfirmPassword)
    {
        if (NewPassword != ConfirmPassword)
        {
            ViewBag.Message = "Passwords do not match!";
            return View();
        }

        var employee = _context.Employees.FirstOrDefault(e => e.EmpNo == EmpNo);

        if (employee == null)
        {
            ViewBag.Message = "Employee number not found!";
            return View();
        }

        // Update the password directly in the database
        employee.Password = NewPassword;
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();

        ViewBag.Message = "Password reset successfully! You can now login with your new password.";

        return View();
    }
}
