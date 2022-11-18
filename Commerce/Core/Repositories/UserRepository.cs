using Commerce.Core.DataTransferObjects;
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
        var filter = Builders<UserDTO>
            .Filter
            .Eq(x => x.UserId, id);

        await _mongoConnection.GetConnection()
            .GetCollection<UserDTO>("Users")
            .DeleteOneAsync(filter);
    }

    public async Task<List<UserDTO>> GetAll()
    {
        return (await _mongoConnection.GetConnection()
            .GetCollection<UserDTO>("Users")
            .FindAsync(new BsonDocument())) //se envia un elemento vacio
            .ToList();
    }

    public async Task<UserDTO> GetById(Guid id)
    {
        var filter = Builders<UserDTO>.Filter.Eq(x => x.UserId, id);

        return await _mongoConnection.GetConnection()
            .GetCollection<UserDTO>("Users")
            .Find(filter).SingleAsync();
    }

    public async Task Insert(UserDTO user)
    {
        await _mongoConnection.GetConnection()
            .GetCollection<UserDTO>("Users").InsertOneAsync(user);
    }

    public async Task Update(UserDTO user)
    {
        var filter = Builders<UserDTO>
            .Filter
            .Eq(x => x.UserId, user.UserId);

        await _mongoConnection.GetConnection()
            .GetCollection<UserDTO>("Users").ReplaceOneAsync(filter, user);
    }
}
