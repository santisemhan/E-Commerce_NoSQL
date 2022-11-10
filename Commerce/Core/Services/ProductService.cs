using Commerce.Core.DataTransferObjects;
using Commerce.Core.Exceptions;
using Commerce.Core.Repositories.Interfaces;
using Commerce.Core.Services.Interfaces;
using System.Net;

namespace Commerce.Core.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;

    public ProductService(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task DeleteProduct(string id)
    {
        await GetProductById(id);
        await productRepository.Delete(id);
    }

    public async Task<List<ProductDTO>> GetAllProducts()
    {
        return await productRepository.GetAll();
    }

    public async Task<ProductDTO> GetProductById(string id)
    {
        var product = await productRepository.GetById(id);
        if (product is null)
        {
            throw new AppException("El producto no existe", HttpStatusCode.NotFound);
        }
        return product;
    }

    public async Task InsertProduct(ProductDTO product)
    {
        await productRepository.Insert(product);
    }

    public async Task UpdateProduct(ProductDTO product, string id)
    {
        await GetProductById(id);
        product.ProductId = new MongoDB.Bson.ObjectId(id);
        await productRepository.Update(product);
    }
}