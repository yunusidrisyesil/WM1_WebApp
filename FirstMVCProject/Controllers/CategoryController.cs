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
            var category = _context.Categories.Include(x => x.Products).ThenInclude(x=> x.OrderDetails)
                            .ThenInclude(x=> x.Order).FirstOrDefault(x => x.CategoryId == id);

            //var category2 = from cat in _context.Categories
            //                join prod in _context.Products on cat.CategoryId equals prod.CategoryId
            //                join odetail in _context.OrderDetails on prod.ProductId equals odetail.ProductId
            //                where cat.CategoryId == id
            //                select cat;
            if(category == null)
            {
                return RedirectToAction(nameof(Index),"Home");
            }
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
