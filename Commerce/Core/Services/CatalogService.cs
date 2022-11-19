using Commerce.Core.DataTransferObjects;
using Commerce.Core.DataTransferObjects.Request;
using Commerce.Core.Exceptions;
using Commerce.Core.Models;
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

    public async Task DeleteCatalog(Guid id)
    {
        await GetCatalogById(id);
        await catalogRepository.Delete(id);
    }

    public async Task<List<ProductCatalog>> GetAllCatalogs()
    {
        return await catalogRepository.GetAll();
    }

    public async Task<ProductCatalog> GetCatalogById(Guid id)
    {
        var catalog =  await catalogRepository.GetById(id);
        if (catalog is null)
        {
            throw new AppException("El catalogo no existe", HttpStatusCode.NotFound);
        }
        return catalog;
    }

    public async Task<List<ProductCatalog>> GetCatalogLogById(Guid id)
    {
        return  await catalogRepository.GetLogById(id);
    }

    public async Task InsertCatalog(ProductCatalogRequestDTO catalogDTO)
    {
        ProductCatalog catalog = new()
        {
            ProductId = catalogDTO.ProductId,
            Price = catalogDTO.Price
        };
        await catalogRepository.Insert(catalog);
        await catalogRepository.InsertLog(catalog);
    }

    public async Task UpdateCatalog(ProductCatalog catalog)
    {
        var catalogo = await catalogRepository.GetById(catalog.Id);
        if (catalogo is null)
        {
            throw new AppException("El catalogo no existe", HttpStatusCode.NotFound);
        }
        await catalogRepository.Update(catalog);
        await catalogRepository.InsertLog(catalog);
    }
}
