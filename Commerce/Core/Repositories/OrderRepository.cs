using Commerce.Core.Models;
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

    public async Task Delete(Guid order)
    {
        var filter = Builders<Order>
            .Filter
            .Eq(x => x.OrderId, order);

        await _mongoConnection.GetConnection()
            .GetCollection<Order>("Orders")
            .DeleteManyAsync(filter);
    }

    public async Task<List<Order>> GetAll()
    {
        return (await _mongoConnection.GetConnection()
           .GetCollection<Order>("Orders")
           .FindAsync(new BsonDocument()))
           .ToList();
    }

    public async Task<List<Order>> GetAllByStatus(bool status)
    {
        var filter = Builders<Order>
            .Filter
            .Eq(x => x.OrderStatus, status);

        return await _mongoConnection.GetConnection()
           .GetCollection<Order>("Orders")
           .Find(filter).ToListAsync();

    }

    public async Task<Order> GetById(Guid id)
    {
        var filter = Builders<Order>
            .Filter
            .Eq(x => x.OrderId, id);

        return await _mongoConnection.GetConnection()
           .GetCollection<Order>("Orders")
           .Find(filter).SingleAsync();
    }

    public async Task Insert(Order order)
    {
        await _mongoConnection.GetConnection()
            .GetCollection<Order>("Orders")
            .InsertOneAsync(order);
    }

    public async Task Update(Order order)
    {
        var filter = Builders<Order>
            .Filter
            .Eq(x => x.OrderId, order.OrderId);

        await _mongoConnection.GetConnection()
            .GetCollection<Order>("Orders")
            .ReplaceOneAsync(filter, order);
    }
}

