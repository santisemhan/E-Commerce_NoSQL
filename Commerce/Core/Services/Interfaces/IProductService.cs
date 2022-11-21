using Commerce.Core.DataTransferObjects;
using Commerce.Core.DataTransferObjects.Request;
using Commerce.Core.Models;

namespace Commerce.Core.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllProducts();

    Task<Product> GetProductById(Guid id);

    Task InsertProduct(ProductDTO productDTO);

    Task UpdateProduct(Product product);

    Task DeleteProduct(Guid id);
}
