using Microsoft.AspNetCore.Mvc;
using IOCL_Training_Module.Data; // Assuming this is your DbContext namespace
using IOCL_Training_Module.Models; // Assuming this is where your Employee model is

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
    public IActionResult Login(string EmpNo)
    {
        var employee = _context.Employees.FirstOrDefault(e => e.EmpNo == EmpNo);

        if (employee != null)
        {
            
            HttpContext.Session.SetString("EmpNo", EmpNo);

            return RedirectToAction("Index", "Dashboard", new { empNo = EmpNo });
        }

        ViewBag.Error = "Invalid Employee Number. Please try again.";
        return View();
    }
}
