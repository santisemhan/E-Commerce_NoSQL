using Commerce.Core.DataTransferObjects;
using Commerce.Core.DataTransferObjects.Request;
using Commerce.Core.Models;

namespace Commerce.Core.Services.Interfaces;

public interface ICatalogService
{
    Task<List<ProductCatalog>> GetAllCatalogs();
    Task<ProductCatalog> GetCatalogById(Guid id);
    Task<List<ProductCatalog>> GetCatalogLogById(Guid id);
    Task InsertCatalog(ProductCatalogDTO catalog);
    Task UpdateCatalog(ProductCatalog catalog);
    Task DeleteCatalog(Guid id);
}
