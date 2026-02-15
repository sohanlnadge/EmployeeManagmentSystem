using EmployeeWebApplication.Data;
using EmployeeWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EmployeeWebApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDBContext _context;

        public EmployeeController(EmployeeDBContext context)
        {
            _context = context;
        }

        // GET: Employee
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();
            return View(employees);
        }

        // GET: Employee/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        // GET: Employee/Delete/5 (Confirmation page)
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // POST: Employee/Delete/5 (Actual delete)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Employee/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            var employee = _context.Employees
                                   .FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // GET: Employee/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
                return RedirectToAction("Index");

            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            var existingEmployee = _context.Employees.Find(id);

            if (id != employee.EmployeeId)
                return NotFound();

            if (ModelState.IsValid)
            {
                
                _context.Employees.Update(employee);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }
    }
}
