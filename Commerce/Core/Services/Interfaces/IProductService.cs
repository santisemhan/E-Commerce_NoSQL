using Commerce.Core.DataTransferObjects;

namespace Commerce.Core.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductDTO>> GetAllProducts();
    Task<ProductDTO> GetProductById(Guid id);
    Task InsertProduct(ProductDTO product);
    Task UpdateProduct(ProductDTO product, Guid id);
    Task DeleteProduct(Guid id);
}
