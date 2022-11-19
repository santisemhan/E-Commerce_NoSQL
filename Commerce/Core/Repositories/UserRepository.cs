using Commerce.Core.Models;
using Commerce.Core.Repositories.Contexts.Interfaces;
using Commerce.Core.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Commerce.Core.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IConnection<IMongoDatabase> _mongoConnection;

    public UserRepository(IConnection<IMongoDatabase> mongoConnection)
    {
        _mongoConnection = mongoConnection;
    }

    public async Task Delete(Guid id)
    {
        var filter = Builders<User>
            .Filter
            .Eq(x => x.UserId, id);

        await _mongoConnection.GetConnection()
            .GetCollection<User>("Users")
            .DeleteOneAsync(filter);
    }

    public async Task<List<User>> GetAll()
    {
        return (await _mongoConnection.GetConnection()
            .GetCollection<User>("Users")
            .FindAsync(new BsonDocument()))
            .ToList();
    }

    public async Task<User> GetById(Guid id)
    {
        var filter = Builders<User>.Filter.Eq(x => x.UserId, id);

        return await _mongoConnection.GetConnection()
            .GetCollection<User>("Users")
            .Find(filter).SingleAsync();
    }

    public async Task Insert(User user)
    {
        await _mongoConnection.GetConnection()
            .GetCollection<User>("Users").InsertOneAsync(user);
    }

    public async Task Update(User user)
    {
        var filter = Builders<User>
            .Filter
            .Eq(x => x.UserId, user.UserId);

        await _mongoConnection.GetConnection()
            .GetCollection<User>("Users")
            .ReplaceOneAsync(filter, user);
    }
}
