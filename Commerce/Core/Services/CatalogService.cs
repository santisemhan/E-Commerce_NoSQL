using Commerce.Core.DataTransferObjects;
using Commerce.Core.Exceptions;
using Commerce.Core.Repositories.Interfaces;
using Commerce.Core.Services.Interfaces;
using System.Net;

namespace Commerce.Core.Services;

public class CatalogService : ICatalogService
{
    private readonly ICatalogRepository catalogRepository;

    public CatalogService(ICatalogRepository catalogRepository)
    {
        this.catalogRepository = catalogRepository;
    }

    public async Task DeleteCatalog(string id)
    {
        await GetCatalogById(id);
        await catalogRepository.Delete(id);
    }

    public async Task<List<ProductCatalogDTO>> GetAllCatalogs()
    {
        return await catalogRepository.GetAll();
    }

    public async Task<ProductCatalogDTO> GetCatalogById(string id)
    {
        var catalog =  await catalogRepository.GetById(id);
        if (catalog is null)
        {
            throw new AppException("El catalogo no existe", HttpStatusCode.NotFound);
        }
        return catalog;
    }

    public Task<List<ProductCatalogDTO>> GetCatalogLogById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task InsertCatalog(ProductCatalogDTO catalog)
    {
        await catalogRepository.Insert(catalog);
        await catalogRepository.InsertLog(catalog);
    }

    public async Task UpdateCatalog(ProductCatalogDTO catalog, string id)
    {
        await GetCatalogById(id);
        catalog.ProductCatalogId = new MongoDB.Bson.ObjectId(id);
        await catalogRepository.Update(catalog);
        await catalogRepository.InsertLog(catalog);
    }
}
