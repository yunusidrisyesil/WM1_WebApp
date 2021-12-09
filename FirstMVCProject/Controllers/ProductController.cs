using FirstMVCProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FirstMVCProject.Controllers
{
    public class ProductController : Controller
    {

        private readonly NorthwindContext _context;

        public ProductController(NorthwindContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? id)
        {
            var product = _context.Products.Include(x => x.Supplier).FirstOrDefault(x => x.ProductId == id);
            if (product == null)
            {
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(product);
        }
    }
}
