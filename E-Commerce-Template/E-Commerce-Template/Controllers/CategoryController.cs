using E_Commerce_Template.Models.Interfaces;
using E_Commerce_Template.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Template.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategory _context;

        public CategoryController(ICategory context)
        {
            _context = context;
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var category = await _context.GetAll();
            return View(category);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name")] Category categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryDto);
            }

            _context.Post(categoryDto);
            return RedirectToAction(nameof(Index));
        }

        // get by ID
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var CategoryDetails = await _context.GetCategoryById(id);
            return View(CategoryDetails);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var CategoryDetails = await _context.GetCategoryById(id);
            return View(CategoryDetails);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("ID, Name")] Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            await _context.Update(id, category);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        // delete
        public async Task<IActionResult> Delete(int id)
        {
            var CategoryDetails = await _context.GetCategoryById(id);
            return View(CategoryDetails);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var CategoryDetails = await _context.GetCategoryById(id);
            await _context.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
