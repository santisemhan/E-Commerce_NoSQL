using Cart.Core.DataTransferObjects;
using Commerce.Core.Repositories.Contexts;
using Commerce.Core.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Commerce.Core.Repositories;

public class UserRepository : IUserRepository
{
    internal MongoDataContext repository = new MongoDataContext();
    private IMongoCollection<UserDTO> Collection;

    public UserRepository()
    {
        Collection = repository.Database.GetCollection<UserDTO>("Users");
    }
    public async Task Delete(string id)
    {
        var filter = Builders<UserDTO>
            .Filter
            .Eq(x => x.UserId, new ObjectId(id));

        await Collection.DeleteOneAsync(filter);
    }

    public async Task<List<UserDTO>> GetAll()
    {
        return await Collection
            .FindAsync(new BsonDocument()) //se envia un elemento vacio
            .Result.ToListAsync();
    }

    public async Task<UserDTO> GetById(string id)
    {
        return await Collection
            .FindAsync(new BsonDocument { { "_id", new ObjectId(id) } })
            .Result.FirstAsync();
    }

    public async Task Insert(UserDTO user)
    {
        await Collection.InsertOneAsync(user);
    }

    public async Task Update(UserDTO user)
    {
        var filter = Builders<UserDTO>
            .Filter
            .Eq(x => x.UserId, user.UserId);

        await Collection.ReplaceOneAsync(filter, user);
    }
}
