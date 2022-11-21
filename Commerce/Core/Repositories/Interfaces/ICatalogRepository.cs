using Commerce.Core.Models;

namespace Commerce.Core.Repositories.Interfaces;

public interface ICatalogRepository
{
    Task<List<ProductCatalog>> GetAll();

    Task<ProductCatalog> GetProductById(Guid id);

    Task<List<ProductCatalog>> GetLogByProductId(Guid id);

    Task Insert(ProductCatalog catalog);

    Task InsertProductLog(ProductCatalog catalog);

    Task Update(ProductCatalog catalog);

    Task Delete(Guid productCatalogId);
}
