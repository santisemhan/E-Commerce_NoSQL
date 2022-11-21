using Commerce.Core.Models;
using Commerce.Core.Repositories.Contexts.Interfaces;
using Commerce.Core.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Commerce.Core.Repositories;

public class CatalogRepository : ICatalogRepository
{
    private readonly IConnection<IMongoDatabase> _mongoConnection;
    private readonly IConnection<Cassandra.ISession> _cassandraConnection;

    public CatalogRepository(IConnection<IMongoDatabase> mongoConnection, IConnection<Cassandra.ISession> cassandraConnection)
    {
        _mongoConnection = mongoConnection;
        _cassandraConnection = cassandraConnection;
    }

    public async Task Delete(Guid productCatalogId)
    {
        var filter = Builders<ProductCatalog>
            .Filter
            .Eq(x => x.Id, productCatalogId);

        await _mongoConnection.GetConnection()
            .GetCollection<ProductCatalog>("ProductCatalogs")
            .DeleteManyAsync(filter);
    }

    public async Task<List<ProductCatalog>> GetAll()
    {
        return (await _mongoConnection.GetConnection()
            .GetCollection<ProductCatalog>("ProductCatalogs")
            .FindAsync(new BsonDocument()))
            .ToList();
    }

    public async Task<ProductCatalog> GetProductById(Guid id)
    {
        var filter = Builders<ProductCatalog>.Filter.Eq(x => x.Id, id);

        return await _mongoConnection.GetConnection()
            .GetCollection<ProductCatalog>("ProductCatalogs")
            .Find(filter).SingleAsync();
    }

    public async Task<List<ProductCatalog>> GetLogByProductId(Guid id)
    {
        var log = new List<ProductCatalog>();

        var query = _cassandraConnection.GetConnection()
            .Execute($@"SELECT * FROM catalog WHERE productid = {id}");

        foreach (var row in query) 
        {
            var catalog = new ProductCatalog(row);
            log.Add(catalog);
        }

        return log;
    }

    public async Task Insert(ProductCatalog product)
    {
        await _mongoConnection.GetConnection()
            .GetCollection<ProductCatalog>("ProductCatalogs")
            .InsertOneAsync(product);
    }

    public async Task InsertProductLog(ProductCatalog product)
    {
        var query = await _cassandraConnection.GetConnection()
                    .PrepareAsync(@"INSERT INTO catalog (id, moment, authorId,productid, price) 
                                    VALUES (?,?,?,?,? )");

        _cassandraConnection.GetConnection()
            .Execute(query.Bind(Guid.NewGuid(),DateTime.Now, product.AuthorId, product.ProductId, product.Price));
    }

    public async Task Update(ProductCatalog product)
    {
        var filter = Builders<ProductCatalog>
            .Filter
            .Eq(x => x.Id, product.Id);

        await _mongoConnection.GetConnection()
            .GetCollection<ProductCatalog>("ProductCatalogs")
            .ReplaceOneAsync(filter, product);
    }
}
