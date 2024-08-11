using ORM_MINI_PROJECT.DTOs;
using ORM_MINI_PROJECT.Excaption;
using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Repositories.Interfaces;
using ORM_MINI_PROJECT.Services.Interfances;

namespace ORM_MINI_PROJECT.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new ProductDto
            {
                Id = p.id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Stock = p.Stock
            }).ToList();
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetSingleAsync(p => p.id == id);
            if (product == null) throw new NotFoundException("Product not found");

            return new ProductDto
            {
                Id = product.id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Stock = product.Stock
            };
        }

        public async Task CreateProductAsync(ProductDto newProduct)
        {
            var product = new Product
            {
                Name = newProduct.Name,
                Price = newProduct.Price,
                Description = newProduct.Description,
                Stock = newProduct.Stock,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _productRepository.CreateAsync(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(ProductDto updatedProduct)
        {
            var product = await _productRepository.GetSingleAsync(p => p.id == updatedProduct.Id);
            if (product == null) throw new NotFoundException("Product not found");

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Description = updatedProduct.Description;
            product.Stock = updatedProduct.Stock;
            product.UpdatedDate = DateTime.UtcNow;

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetSingleAsync(p => p.id == id);
            if (product == null) throw new NotFoundException("Product not found");

            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();
        }
    }
}
