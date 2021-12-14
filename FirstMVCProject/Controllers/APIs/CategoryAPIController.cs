using FirstMVCProject.Models;
using FirstMVCProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FirstMVCProject.Controllers.APIs
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryAPIController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public CategoryAPIController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetCategories()
        {
            try
            {
                var categories = _context.Categories
                    .Include(x => x.Products)
                    .OrderBy(x => x.CategoryName)
                    .Select(x => new CategoryViewModel()
                    {
                        CategoryId = x.CategoryId,
                        CategoryName = x.CategoryName,
                        Description = x.Description,
                        ProductCount = x.Products.Count()
                    })
                    .ToList();
                return Ok(categories);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Categories.Add(new Category
            {
                CategoryName = model.CategoryName,
                Description=model.Description,
            });

            try
            {
                _context.SaveChanges();
                return Ok("Category added succesfully");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("~/api/categoryapi/updatecategory/{id?}")] //Custom Route
        public IActionResult UpdateCategory(int? id, CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var category = _context.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (category == null) return NotFound("Category not found");
            
            category.CategoryName = model.CategoryName;
            category.Description = model.Description;

            try
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                return Ok(new {
                    Message = "Category update successfully",
                    Model = category,
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult DeleteCategory(int? id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.CategoryId == id);

            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return Ok("Category deleted successfuly");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
