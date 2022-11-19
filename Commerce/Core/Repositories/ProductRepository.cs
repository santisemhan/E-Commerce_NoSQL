using Commerce.Core.Models;
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
        var filter = Builders<Product>
            .Filter
            .Eq(x => x.ProductId, id);

        await _mongoConnection.GetConnection()
            .GetCollection<Product>("Products")
            .DeleteOneAsync(filter);
    }

    public async Task<List<Product>> GetAll()
    {
        return (await _mongoConnection.GetConnection()
            .GetCollection<Product>("Products")
            .FindAsync(new BsonDocument()))
            .ToList();
    }

    public async Task<Product> GetById(Guid id)
    {
        var filter = Builders<Product>.Filter.Eq(x => x.ProductId, id);

        return await _mongoConnection.GetConnection()
            .GetCollection<Product>("Products")
            .Find(filter).SingleOrDefaultAsync();
    }

    public async Task Insert(Product product)
    {
        await _mongoConnection.GetConnection()
            .GetCollection<Product>("Products")
            .InsertOneAsync(product);
    }

    public async Task Update(Product product)
    {
        var filter = Builders<Product>
            .Filter
            .Eq(x => x.ProductId, product.ProductId);

        await _mongoConnection.GetConnection()
            .GetCollection<Product>("Products")
            .ReplaceOneAsync(filter, product);
    }
}
