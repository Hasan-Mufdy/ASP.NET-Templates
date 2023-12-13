using E_Commerce_Template.Models.Entities;
using E_Commerce_Template.Models.Interfaces;
using E_Commerce_Template.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Template.Controllers
{
    public class UserstorefrontController : Controller
    {
        private readonly ICategory _categoryContext;
        private readonly IProduct _productContext;
        private readonly SignInManager<AuthUser> _signInManager;

        public UserstorefrontController(ICategory categoryContext, IProduct productContext, SignInManager<AuthUser> signInManager)
        {
            _categoryContext = categoryContext;
            _productContext = productContext;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            // await _emailService.sendEmailThankYou();
            var prod = await _productContext.GetAll();
            return View(prod);
        }

        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            List<Product> products;
            if (categoryId > 0)
            {
                var category = await _categoryContext.GetCategoryById(categoryId);
                if (category != null)
                {
                    products = await _productContext.GetProductsByCategory(categoryId);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                products = await _productContext.GetAll();
            }

            return View(products);
        }

        public async Task<IActionResult> AllProducts(string searchTerm)
        {
            List<Product> products = await _productContext.GetAll();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                products = products.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var ProductDetails = await _productContext.GetProductById(id);
            return View(ProductDetails);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var ProductDetails = await _productContext.GetProductById(id);
            return View(ProductDetails);
        }


        [Authorize(Roles = "Guest")]
        public async Task<IActionResult> ViewCart()
        {
            //var cartItemsJson = HttpContext.Request.Cookies["cartItems"];

            //if (!string.IsNullOrEmpty(cartItemsJson))
            //{
            //    var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(cartItemsJson);
            //    return View(cartItems);
            //}

            //return View(new List<CartItem>());
            List<Product> products = await _productContext.GetAll();

            //if (!string.IsNullOrWhiteSpace(searchTerm))
            //{
            //    products = products.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            //}

            return View(products);
        }
    }
}
