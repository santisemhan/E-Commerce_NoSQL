using Commerce.Core.Models;
using Commerce.Core.Repositories.Contexts.Interfaces;
using Commerce.Core.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using Cassandra;

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

    public async Task<ProductCatalog> GetById(Guid id)
    {
        var filter = Builders<ProductCatalog>.Filter.Eq(x => x.Id, id);

        return await _mongoConnection.GetConnection()
            .GetCollection<ProductCatalog>("ProductCatalogs")
            .Find(filter).SingleAsync();
    }

    public async Task<List<ProductCatalog>> GetLogById(Guid id)
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

    public async Task Insert(ProductCatalog catalog)
    {
        await _mongoConnection.GetConnection()
            .GetCollection<ProductCatalog>("ProductCatalogs")
            .InsertOneAsync(catalog);
    }

    public async Task InsertLog(ProductCatalog catalog)
    {
        var productCatalogId = Guid.NewGuid();

        var query = await _cassandraConnection.GetConnection()
                    .PrepareAsync(@"INSERT INTO catalog (id, productid, price) 
                                    VALUES (?,?,?)");

        _cassandraConnection.GetConnection()
            .Execute(query.Bind(productCatalogId,catalog.ProductId,catalog.Price));
    }

    public async Task Update(ProductCatalog catalog)
    {
        var filter = Builders<ProductCatalog>
            .Filter
            .Eq(x => x.Id, catalog.Id);

        await _mongoConnection.GetConnection()
            .GetCollection<ProductCatalog>("ProductCatalogs")
            .ReplaceOneAsync(filter,catalog);
    }
}
