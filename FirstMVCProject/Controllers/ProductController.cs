using FirstMVCProject.Models;
using FirstMVCProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstMVCProject.Controllers
{
    public class ProductController : Controller
    {

        private readonly NorthwindContext _context;
        private int _pageSize = 10;

        public ProductController(NorthwindContext context)
        {
            _context = context;
        }

        public IActionResult Detail(int? id)
        {
            //var product = _context.Products.Include(x => x.Supplier).FirstOrDefault(x => x.ProductId == id);
            //if (product == null)
            //{
            //    return RedirectToAction(nameof(Index), "Home");
            //}
            //return View(product);

            var data = _context.Products.Include(x => x.Supplier).Include(x => x.Category).FirstOrDefault(x => x.ProductId == id);
            if (data == null) return RedirectToAction(nameof(Index));

            var model = new ProductViewModel
            {
                ProductId = data.ProductId,
                CategoryName = data.Category?.CategoryName,
                CompanyName = data.Supplier?.CompanyName,
                ProductName = data.ProductName,
                UnitPrice = data.UnitPrice,
                SupplierId = data.SupplierId,
                CategoryId = data.CategoryId,
            };
            return View(model);

        }

        public IActionResult Index(int? page = 1)
        {
            var model = _context.Products.Include(x => x.Category)
                                         .Include(x => x.Supplier)
                                         .OrderBy(x => x.Category.CategoryName)
                                         .ThenBy(x => x.ProductName)
                                         .Skip((page.GetValueOrDefault() - 1) * _pageSize)
                                         .Take(_pageSize)
                                         .ToList();

            ViewBag.Page = page.GetValueOrDefault(1);
            ViewBag.Limit =  (int)Math.Ceiling(_context.Products.Count() / (double)_pageSize);
            //ViewBag.Categories = _context.Categories.OrderBy(x => x.CategoryName).ToList();
            //ViewBag.Product = _context.Suppliers.OrderBy(x => x.CompanyName).ToList();

            return View(model);
        }

        public IActionResult Update(int? id)
        {
            var data = _context.Products.Include(x => x.Supplier).Include(x => x.Category).FirstOrDefault(x => x.ProductId == id);
            if (data == null) return RedirectToAction(nameof(Index));

            var model = new ProductViewModel
            {
                ProductId = data.ProductId,
                CategoryName = data.Category?.CategoryName,
                CompanyName = data.Supplier?.CompanyName,
                ProductName = data.ProductName,
                UnitPrice = data.UnitPrice,
                SupplierId = data.SupplierId,
                CategoryId = data.CategoryId,
            };

            
            

            ViewBag.CategoryList = GetCategoryList();
            ViewBag.SupplierList = GetSupplierList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryList = GetCategoryList();
                ViewBag.SupplierList = GetSupplierList();
                return View(model);
            }

            var product = _context.Products.FirstOrDefault(x => x.ProductId == model.ProductId);
            if (product == null)
                return RedirectToAction(nameof(Index));

            try
            {
                product.ProductId = model.ProductId;
                product.ProductName = model.ProductName;
                product.UnitPrice = model.UnitPrice;
                product.SupplierId = model.SupplierId;
                product.CategoryId = model.CategoryId;

                _context.Products.Update(product);
                _context.SaveChanges();
                TempData["Mesaj"] = $"{product.ProductName} başarıyla güncellenmiştir";
                return RedirectToAction(nameof(Detail), new { id = product.ProductId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.CategoryList = GetCategoryList();
                ViewBag.SupplierList = GetSupplierList();
                return View(model);
            }
        }

        private List<SelectListItem> GetCategoryList()
        {
            var categories = _context.Categories.OrderBy(x => x.CategoryName).ToList();
            var categoryList = new List<SelectListItem>()
            {
                new SelectListItem("No Category", null),
            };

            foreach (var category in categories)
            {
                categoryList.Add(new SelectListItem(category.CategoryName, category.CategoryId.ToString()));
            }

            return categoryList;
        }

        private List<SelectListItem> GetSupplierList()
        {
            var suppliers = _context.Suppliers.OrderBy(x => x.CompanyName).ToList();
            var supplierList = new List<SelectListItem>()
            {
                new SelectListItem("No Supllier",null)
            };
            foreach (var supplier in suppliers)
            {
                supplierList.Add(new SelectListItem(supplier.CompanyName, supplier.SupplierId.ToString()));
            }

            return supplierList;
        }

        public IActionResult Delete(int? id)
        {
            var deleted = _context.Products.FirstOrDefault(x => x.ProductId == id);
            if (deleted == null) { return RedirectToAction(nameof(Index)); }

            _context.Products.Remove(deleted);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
