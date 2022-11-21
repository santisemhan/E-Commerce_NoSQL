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

    public async Task DeleteProductCatalog(Guid id)
    {
        await GetProductCatalogById(id);
        await catalogRepository.Delete(id);
    }

    public async Task<List<ProductCatalog>> GetAllProductsCatalog()
    {
        return await catalogRepository.GetAll();
    }

    public async Task<ProductCatalog> GetProductCatalogById(Guid id)
    {
        var product =  await catalogRepository.GetProductById(id);
        if (product is null)
        {
            throw new AppException("El catalogo no existe", HttpStatusCode.NotFound);
        }
        return product;
    }

    public async Task<List<ProductCatalog>> GetProductCatalogLogById(Guid id)
    {
        return  await catalogRepository.GetLogByProductId(id);
    }

    public async Task InsertProductCatalog(ProductCatalogDTO catalogDTO)
    {
        ProductCatalog catalog = new()
        {
            AuthorId = catalogDTO.AuthorId, 
            ProductId = catalogDTO.ProductId,
            Moment = DateTime.Now,
            Price = catalogDTO.Price
        };
        await catalogRepository.Insert(catalog);
        await catalogRepository.InsertProductLog(catalog);
    }

    public async Task UpdateProductCatalog(ProductCatalog catalog)
    {
        var catalogo = await catalogRepository.GetProductById(catalog.Id);
        if (catalogo is null)
        {
            throw new AppException("El catalogo no existe", HttpStatusCode.NotFound);
        }
        await catalogRepository.Update(catalog);
        await catalogRepository.InsertProductLog(catalog);
    }
}
