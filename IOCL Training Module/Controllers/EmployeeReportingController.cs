using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IOCL_Training_Module.Data;
using IOCL_Training_Module.Models;
using System.Threading.Tasks;
using System.Linq;

namespace IOCL_Training_Module.Controllers
{
    public class EmployeeReportingController : Controller
    {
        private readonly DatabaseContext _context;

        public EmployeeReportingController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: EmployeeReporting
        public async Task<IActionResult> Index()
        {
            var reportingList = _context.Reportings
                .Include(r => r.Employee)
                .Include(r => r.Manager);
            return View(await reportingList.ToListAsync());
        }

        // GET: EmployeeReporting/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var employeeReporting = await _context.Reportings
                .Include(r => r.Employee)
                .Include(r => r.Manager)
                .FirstOrDefaultAsync(m => m.SrNo == id);

            if (employeeReporting == null) return NotFound();

            return View(employeeReporting);
        }

        // GET: EmployeeReporting/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeReporting/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SrNo,EmpNo,Reporting")] EmployeeReporting employeeReporting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeReporting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeReporting);
        }

        // GET: EmployeeReporting/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var employeeReporting = await _context.Reportings.FindAsync(id);
            if (employeeReporting == null) return NotFound();

            return View(employeeReporting);
        }

        // POST: EmployeeReporting/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SrNo,EmpNo,Reporting")] EmployeeReporting employeeReporting)
        {
            if (id != employeeReporting.SrNo) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeReporting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeReportingExists(employeeReporting.SrNo)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeReporting);
        }

        // GET: EmployeeReporting/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var employeeReporting = await _context.Reportings
                .Include(r => r.Employee)
                .Include(r => r.Manager)
                .FirstOrDefaultAsync(m => m.SrNo == id);

            if (employeeReporting == null) return NotFound();

            return View(employeeReporting);
        }

        // POST: EmployeeReporting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeReporting = await _context.Reportings.FindAsync(id);
            if (employeeReporting != null)
            {
                _context.Reportings.Remove(employeeReporting);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeReportingExists(int id)
        {
            return _context.Reportings.Any(e => e.SrNo == id);
        }
    }
}
