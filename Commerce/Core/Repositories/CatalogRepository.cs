using Commerce.Core.DataTransferObjects;
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

    public async Task Delete(string catalog)
    {
        var filter = Builders<ProductCatalogDTO>
            .Filter
            .Eq(x => x.ProductId, catalog);

        await _mongoConnection.GetConnection()
            .GetCollection<ProductCatalogDTO>("ProductCatalogs")
            .DeleteManyAsync(filter);
    }

    public async Task<List<ProductCatalogDTO>> GetAll()
    {
        return (await _mongoConnection.GetConnection()
            .GetCollection<ProductCatalogDTO>("ProductCatalogs")
            .FindAsync(new BsonDocument()))
            .ToList();
    }

    public async Task<ProductCatalogDTO> GetById(string id)
    {
        return (await _mongoConnection.GetConnection()
           .GetCollection<ProductCatalogDTO>("ProductCatalogs")
           .FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }))
           .First();
    }

    public async Task<List<ProductCatalogDTO>> GetLogById(Guid id)
    {

        var log = new List<ProductCatalogDTO>();

        var query = _cassandraConnection.GetConnection()
            .Execute($@"SELECT * FROM catalog WHERE productCatalogIdGuid = {id}");

        foreach (var row in query) {
            var catalog = new ProductCatalogDTO(row);
            log.Add(catalog);
        }

        return log;
    }

    public async Task Insert(ProductCatalogDTO catalog)
    {
        await _mongoConnection.GetConnection()
            .GetCollection<ProductCatalogDTO>("ProductCatalogs")
            .InsertOneAsync(catalog);
    }

    public async Task InsertLog(ProductCatalogDTO catalog)
    {
        var productCatalogId = Guid.NewGuid();

        var query = await _cassandraConnection.GetConnection()
                    .PrepareAsync(@"INSERT INTO catalog (id, productid, price) 
                                    VALUES (?,?,?)");

        _cassandraConnection.GetConnection()
            .Execute(query.Bind(productCatalogId,catalog.ProductId,catalog.Price));
    }

    public async Task Update(ProductCatalogDTO catalog)
    {
        var filter = Builders<ProductCatalogDTO>
            .Filter
            .Eq(x => x.ProductCatalogId, catalog.ProductCatalogId);

        await _mongoConnection.GetConnection()
            .GetCollection<ProductCatalogDTO>("ProductCatalog")
            .ReplaceOneAsync(filter, catalog);
    }
}
