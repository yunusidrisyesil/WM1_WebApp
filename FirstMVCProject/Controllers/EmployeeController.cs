using FirstMVCProject.Models;
using FirstMVCProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FirstMVCProject.Controllers
{
    public class EmployeeController : Controller
    {
        NorthwindContext _context;

        public EmployeeController(NorthwindContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Employees.OrderBy(x => x.EmployeeId).ToList();
            return View(data);
        }

        public IActionResult Detail(int? id)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.EmployeeId == id);
            if (employee == null) RedirectToAction(nameof(Index));

            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            var employee = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            try
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction(nameof(Detail),new {id = employee.EmployeeId});
            }
            catch (System.Exception)
            {
                ModelState.AddModelError(string.Empty, $"An error accured while adding {employee.FirstName}");
                return View(model);
            }
        }
    }
}
