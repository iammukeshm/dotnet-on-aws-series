using Amazon.DynamoDBv2.DataModel;
using Products.Api.Data;
using Products.Api.Dtos;

namespace Products.Api.Services;

public class ProductService(IDynamoDBContext ddb) : IProductService
{
    public async Task<Product> CreateProductAsync(CreateProductRequest request)
    {
        var product = await ddb.LoadAsync<Product>(request.Id);
        if (product != null) throw new Exception($"Product with Id {request.Id} Already Exists");
        var productToCreate = new Product()
        {
            Id = request.Id,
            Name = request.Name,
            Price = request.Price,
        };
        await ddb.SaveAsync(productToCreate);
        return productToCreate;
    }

    public async Task DeleteProductAsync(string id)
    {
        var product = await ddb.LoadAsync<Product>(id);
        if (product == null) throw new Exception($"Product with Id {id} Not Found");
        await ddb.DeleteAsync(product);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var products = await ddb.ScanAsync<Product>(default).GetRemainingAsync();
        return products;
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
        var product = await ddb.LoadAsync<Product>(id);
        if (product == null) throw new Exception($"Product with Id {id} Not Found");
        return product;
    }

    public async Task<Product> UpdateProductAsync(UpdateProductRequest request)
    {
        var product = await ddb.LoadAsync<Product>(request.Id);
        if (product == null) throw new Exception($"Product with Id {request.Id} Not Found");
        var productToUpdate = new Product()
        {
            Id = request.Id,
            Name = request.Name,
            Price = request.Price,
        };
        await ddb.SaveAsync(productToUpdate);
        return productToUpdate;
    }
}
