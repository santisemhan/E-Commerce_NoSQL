using Commerce.Core.DataTransferObjects;

namespace Commerce.Core.Services.Interfaces;

public interface ICatalogService
{
    Task<List<ProductCatalogDTO>> GetAllCatalogs();
    Task<ProductCatalogDTO> GetCatalogById(Guid id);
    Task<List<ProductCatalogDTO>> GetCatalogLogById(Guid id);
    Task InsertCatalog(ProductCatalogDTO catalog);
    Task UpdateCatalog(ProductCatalogDTO catalog, Guid id);
    Task DeleteCatalog(Guid id);
}
