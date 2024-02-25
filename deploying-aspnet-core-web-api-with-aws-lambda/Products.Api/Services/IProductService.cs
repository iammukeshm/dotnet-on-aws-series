using Products.Api.Data;
using Products.Api.Dtos;

namespace Products.Api.Services;

public interface IProductService
{
    public Task<Product> CreateProductAsync(CreateProductRequest request);
    public Task<Product> GetProductByIdAsync(string id);
    public Task<IEnumerable<Product>> GetAllProductsAsync();
    public Task DeleteProductAsync(string id);
    public Task<Product> UpdateProductAsync(UpdateProductRequest request);
}
