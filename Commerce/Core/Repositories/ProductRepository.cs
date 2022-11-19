using Commerce.Core.DataTransferObjects;
using Commerce.Core.Repositories.Contexts.Interfaces;
using Commerce.Core.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Commerce.Core.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IConnection<IMongoDatabase> _mongoConnection;

    public ProductRepository(IConnection<IMongoDatabase> mongoConnection)
    {
        _mongoConnection = mongoConnection;
    }

    public async Task Delete(Guid id)
    {
        var filter = Builders<ProductDTO>
            .Filter
            .Eq(x => x.ProductId, id);

        await _mongoConnection.GetConnection()
            .GetCollection<ProductDTO>("Products")
            .DeleteOneAsync(filter);
    }

    public async Task<List<ProductDTO>> GetAll()
    {
        return (await _mongoConnection.GetConnection()
            .GetCollection<ProductDTO>("Products")
            .FindAsync(new BsonDocument()))
            .ToList();
    }

    public async Task<ProductDTO> GetById(Guid id)
    {
        var filter = Builders<ProductDTO>.Filter.Eq(x => x.ProductId, id);

        return await _mongoConnection.GetConnection()
            .GetCollection<ProductDTO>("Products")
            .Find(filter).SingleOrDefaultAsync();
    }

    public async Task Insert(ProductDTO product)
    {
        await _mongoConnection.GetConnection()
            .GetCollection<ProductDTO>("Products")
            .InsertOneAsync(product);
    }

    public async Task Update(ProductDTO product)
    {
        var filter = Builders<ProductDTO>
            .Filter
            .Eq(x => x.ProductId, product.ProductId);

        await _mongoConnection.GetConnection()
            .GetCollection<ProductDTO>("Products")
            .ReplaceOneAsync(filter, product);
    }
}
