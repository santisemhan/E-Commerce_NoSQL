using Commerce.Core.DataTransferObjects.Request;
using Commerce.Core.Models;

namespace Commerce.Core.Services.Interfaces;

public interface ICatalogService
{
    Task<List<ProductCatalog>> GetAllProductsCatalog();

    Task<ProductCatalog> GetProductCatalogById(Guid id);

    Task<List<ProductCatalog>> GetProductCatalogLogById(Guid id);

    Task InsertProductCatalog(ProductCatalogDTO catalog);

    Task UpdateProductCatalog(ProductCatalog catalog);

    Task DeleteProductCatalog(Guid id);
}
