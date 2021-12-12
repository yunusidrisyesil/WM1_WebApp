using FirstMVCProject.Models;
using FirstMVCProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
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
                Title = model.Title,
                BirthDate = model.BirthDate,
                HireDate = DateTime.Now,
                Address = model.Address,
                City = model.City,
                Country = model.Country,
                HomePhone = model.HomePhone,
                TitleOfCourtesy = model.TitleOfCourtesy,
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

        public IActionResult Delete(int? id)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.EmployeeId == id);

            try
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                return RedirectToAction(nameof(Detail),new { id = employee.EmployeeId});
                throw;
            }
            TempData["Deleted_Employee"] = employee.FirstName + employee.LastName;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.EmployeeId == id);
            if (employee == null) return RedirectToAction(nameof(Index));

            var model = new EmployeeViewModel()
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = (DateTime)employee.BirthDate,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy,
                Address = employee.Address,
                City = employee.City,
                Country = employee.Country,
                HireDate = (DateTime)employee.HireDate,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var employee = _context.Employees.FirstOrDefault(x => x.EmployeeId == model.EmployeeId);

            try
            {
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.BirthDate = model.BirthDate;
                employee.Title = model.Title;
                employee.Address = model.Address;
                employee.City = model.City;
                employee.Country = model.Country; 
                employee.HomePhone = model.HomePhone;
                employee.TitleOfCourtesy= model.TitleOfCourtesy;

                _context.Employees.Update(employee);
                _context.SaveChanges();

                return RedirectToAction(nameof(Detail), new { id = employee.EmployeeId });
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(model);
        }
    }
}
