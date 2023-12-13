namespace E_Commerce_Template.Models.Interfaces
{
    public interface ICategory
    {
        Task<List<Category>> GetAll();
        Task<Category> GetCategoryById(int id);
        Task<Category> Post(Category category);
        Task<Category> Update(int id, Category category);
        Task Delete(int id);
    }
}
