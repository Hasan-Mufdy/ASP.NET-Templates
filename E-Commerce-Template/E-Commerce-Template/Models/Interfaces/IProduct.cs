namespace E_Commerce_Template.Models.Interfaces
{
    public interface IProduct
    {
        Task<List<Product>> GetAll();
        Task<Product> GetProductById(int id);
        Task<Product> Post(Product product, IFormFile file);
        Task Update(int id, Product product, IFormFile file);
        Task Delete(int id);

        Task<List<Product>> GetProductsByCategory(int categoryId);
    }
}
