using E_Commerce_Template.Models.Interfaces;
using E_Commerce_Template.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Template.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _context;
        private readonly ICategory _categoryContext;
        private readonly IConfiguration _configuration;

        public ProductController(IProduct context, ICategory categoryContext, IConfiguration configuration)
        {
            _context = context;
            _categoryContext = categoryContext;
            _configuration = configuration;
        }

        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string searchTerm)
        {
            List<Product> products = await _context.GetAll();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                products = products.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(products);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            //Product product = new Product();
            //return View(product);
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, CategoryId, Description, Price, Size, ImageUrl")] Product product, IFormFile file)//up
        {

            ModelState.Remove("file");

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            //return Content("The product is added successfully");
            await _context.Post(product, file);
            return RedirectToAction(nameof(Index));
        }

        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            List<Product> products;
            if (categoryId > 0)
            {
                var category = await _categoryContext.GetCategoryById(categoryId);
                if (category != null)
                {
                    products = await _context.GetProductsByCategory(categoryId);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                products = await _context.GetAll();
            }

            return View(products);
        }

        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> Details(int id)
        {
            var ProductDetails = await _context.GetProductById(id);
            return View(ProductDetails);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var ProductDetails = await _context.GetProductById(id);
            return View(ProductDetails);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile file)
        {
            ModelState.Remove("file");

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            await _context.Update(id, product, file);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        // delete
        public async Task<IActionResult> Delete(int id)
        {
            var ProductDetails = await _context.GetProductById(id);
            return View(ProductDetails);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var ProductDetails = await _context.GetProductById(id);
            await _context.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
