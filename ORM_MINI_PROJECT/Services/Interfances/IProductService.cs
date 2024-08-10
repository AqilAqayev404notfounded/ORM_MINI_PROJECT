using ORM_MINI_PROJECT.DTOs;

namespace ORM_MINI_PROJECT.Services.Interfances;

public interface IProductService
{
    Task<List<ProductDto>> GetAllProductsAsync();
    Task CreateProductAsync(ProductDto newProduct);
    Task UpdateProductAsync(ProductDto updatedProduct);
    Task DeleteProductAsync(int id);
    Task<ProductDto> GetProductByIdAsync(int id);
}
