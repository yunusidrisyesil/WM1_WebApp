using FirstMVCProject.Models;
using FirstMVCProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FirstMVCProject.Controllers.APIs
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public ProductAPIController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel model)
        {
            var product = new Product()
            {
                CategoryId = model.CategoryId,
                ProductName = model.ProductName,
                UnitPrice = model.UnitPrice,
            };

            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();

                return Ok(new { 
                    Message = "Product added successfuly",
                    Model = product
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest($"An error accured: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("~/api/productapi/delete/{id?}")]
        public IActionResult Delete(int? id = 0)
        {
            var model = _context.Products.FirstOrDefault(x => x.ProductId == id);
            if (model == null) return NotFound("Product not found");
            try
            {
                _context.Products.Remove(model);
                _context.SaveChanges();
                return Ok(new { 
                    Message = $"{model.ProductName} deleted succesfully",
                    Name = model.ProductName,
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest($"An error occured: {ex.Message})");
            }
        }
    }
}
