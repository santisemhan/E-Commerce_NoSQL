using Commerce.Core.DataTransferObjects;
using Commerce.Core.Repositories.Contexts.Interfaces;
using Commerce.Core.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Commerce.Core.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IConnection<IMongoDatabase> _mongoConnection;

    public OrderRepository(IConnection<IMongoDatabase> mongoConnection)
    {
        _mongoConnection = mongoConnection;
    }

    public async Task Delete(string order)
    {
        var filter = Builders<OrderDTO>
           .Filter
           .Eq(x => x.OrderId, new ObjectId(order));

        await _mongoConnection.GetConnection()
            .GetCollection<OrderDTO>("Orders")
            .DeleteManyAsync(filter);
    }

    public async Task<List<OrderDTO>> GetAll()
    {
        return (await _mongoConnection.GetConnection()
           .GetCollection<OrderDTO>("Orders")
           .FindAsync(new BsonDocument()))
           .ToList();
    }

    public async Task<OrderDTO> GetById(string id)
    {
        return (await _mongoConnection.GetConnection()
           .GetCollection<OrderDTO>("Orders")
           .FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }))
           .First();
    }

    public async Task Insert(OrderDTO order)
    {
        await _mongoConnection.GetConnection()
            .GetCollection<OrderDTO>("Orders")
            .WithWriteConcern(WriteConcern.W1)
            .InsertOneAsync(order);
    }

    public async Task Update(OrderDTO order)
    {
        var filter = Builders<OrderDTO>
            .Filter
            .Eq(x => x.OrderId, order.OrderId);

        await _mongoConnection.GetConnection()
            .GetCollection<OrderDTO>("Orders")
            .ReplaceOneAsync(filter, order);
    }
}
}
