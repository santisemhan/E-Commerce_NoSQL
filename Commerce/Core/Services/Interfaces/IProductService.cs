using Commerce.Core.DataTransferObjects;

namespace Commerce.Core.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductDTO>> GetAllProducts();
    Task<ProductDTO> GetProductById(string id);
    Task InsertProduct(ProductDTO product);
    Task UpdateProduct(ProductDTO product, string id);
    Task DeleteProduct(string id);
}
