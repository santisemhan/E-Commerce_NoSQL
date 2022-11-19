using Commerce.Core.Models;

namespace Commerce.Core.Repositories.Interfaces;

public interface ICatalogRepository
{
    Task<List<ProductCatalog>> GetAll();

    Task<ProductCatalog> GetById(Guid id);

    Task<List<ProductCatalog>> GetLogById(Guid id);

    Task Insert(ProductCatalog catalog);

    Task InsertLog(ProductCatalog catalog);

    Task Update(ProductCatalog catalog);

    Task Delete(Guid productCatalogId);
}
