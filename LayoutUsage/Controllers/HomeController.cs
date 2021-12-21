using LayoutUsage.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LayoutUsage.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            var model = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "Watermelon",
                    Price = 7
                },
                new Product()
                {
                    Id = 2,
                    Name = "Plum",
                    Price = 9
                }
            };
            return View(model);
        }
    }
}
