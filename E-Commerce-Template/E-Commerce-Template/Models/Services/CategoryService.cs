using E_Commerce_Template.Data;
using E_Commerce_Template.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Template.Models.Services
{
    public class CategoryService : ICategory
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _context.Categories.Where(c => c.Id == id).SingleOrDefaultAsync();
            return category;
        }

        public async Task<Category> Post(Category categoryDto)
        {
            var category = new Category()
            {
                Name = categoryDto.Name,
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> Update(int id, Category newCategory)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            category.Name = newCategory.Name;
            await _context.SaveChangesAsync();
            return newCategory;
        }
    }
}
