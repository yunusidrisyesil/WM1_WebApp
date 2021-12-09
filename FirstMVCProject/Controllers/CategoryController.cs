using FirstMVCProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstMVCProject.Controllers
{
    public class CategoryController : Controller
    {

        private readonly NorthwindContext _context;

        public CategoryController(NorthwindContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Categories.Include(x => x.Products).OrderBy(x => x.CategoryName).ToList();
            return View(data);
        }

        public IActionResult Detail(int? id)
        {
            var category = _context.Categories.Include(x => x.Products).FirstOrDefault(x => x.CategoryId == id);
            if(category == null)
            {
                return RedirectToAction(nameof(Index),"Home");
            }
            return View(category);
        }
    }
}
