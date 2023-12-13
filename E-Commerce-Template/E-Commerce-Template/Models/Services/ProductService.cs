using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using E_Commerce_Template.Data;
using Microsoft.EntityFrameworkCore;
using E_Commerce_Template.Models.Interfaces;

namespace E_Commerce_Template.Models.Services
{
    public class ProductService : IProduct
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public ProductService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task Delete(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products
                .Include(c => c.Category)
                .ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _context.Products
            .Include(c => c.Category)
            .Where(p => p.Id == id).SingleOrDefaultAsync();
            return product;
        }

        public async Task<List<Product>> GetProductsByCategory(int categoryId)
        {
            var products = _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price
                })
                .ToList();

            return products;
        }

        public async Task<Product> Post(Product productDto, IFormFile file)
        {

            var URL = await UploudFile(file);

            var product = new Product()
            {
                Name = productDto.Name,
                CategoryId = productDto.CategoryId,
                Description = productDto.Description,
                Price = productDto.Price,
                ImageUrl = URL
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task Update(int id, Product newProduct, IFormFile file)
        {



            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            product.Name = newProduct.Name;
            product.CategoryId = newProduct.CategoryId;
            product.Description = newProduct.Description;
            product.Price = newProduct.Price;

            if (file != null)
            {
                var URL = await UploudFile(file);
                product.ImageUrl = URL;
            }
            else
            {
                product.ImageUrl = product.ImageUrl;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<string> UploudFile(IFormFile file)
        {
            //var URL = "https://ecommerceprojectimages.blob.core.windows.net/images/noimage.png";
            var URL = "../../noimage.png";
            if (file != null)
            {
                BlobContainerClient blobContainerClient =
                    new BlobContainerClient(_configuration.GetConnectionString("StorageAccount"), "images");

                await blobContainerClient.CreateIfNotExistsAsync();

                BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);

                using var fileStream = file.OpenReadStream();

                BlobUploadOptions blobUploadOptions = new BlobUploadOptions()
                {
                    HttpHeaders = new BlobHttpHeaders { ContentType = file.ContentType }
                };

                if (!blobClient.Exists())
                {
                    await blobClient.UploadAsync(fileStream, blobUploadOptions);
                }
                URL = blobClient.Uri.ToString();
            }
            return URL;
        }
    }
}
