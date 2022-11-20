using AutoMapper;
using Commerce.Core.DataTransferObjects;
using Commerce.Core.DataTransferObjects.Request;
using Commerce.Core.Exceptions;
using Commerce.Core.Models;
using Commerce.Core.Repositories.Interfaces;
using Commerce.Core.Services.Interfaces;
using System.Net;

namespace Commerce.Core.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;
    public ProductService(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;

    }

    public async Task DeleteProduct(Guid id)
    {
        await GetProductById(id);
        await productRepository.Delete(id);
    }

    public async Task<List<Product>> GetAllProducts()
    {
        return await productRepository.GetAll();
    }

    public async Task<Product> GetProductById(Guid id)
    {
        var product = await productRepository.GetById(id);
        if (product is null)
        {
            throw new AppException("El producto no existe", HttpStatusCode.NotFound);
        }
        return product;
    }

    public async Task InsertProduct(ProductDTO productDTO)
    {
        Product product = new()
        {
            ProductName = productDTO.ProductName,
            ImagesURL = productDTO.ImagesURL,
            MainImage = productDTO.MainImage,
            Description = productDTO.MainImage,
            Comments = productDTO.Comments,
            Stock = productDTO.Stock
        };
        await productRepository.Insert(product);
    }

    public async Task UpdateProduct(Product productDTO)
    {
        await GetProductById(productDTO.ProductId);
        await productRepository.Update(productDTO);
    }
}