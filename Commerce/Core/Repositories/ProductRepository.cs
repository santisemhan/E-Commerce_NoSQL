using Commerce.Core.DataTransferObjects;
using Commerce.Core.Repositories.Contexts;
using Commerce.Core.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Commerce.Core.Repositories;

public class ProductRepository : IProductRepository
{
    internal MongoDataContext repository = new MongoDataContext();
    private IMongoCollection<ProductDTO> Collection;

    public ProductRepository()
    {
        Collection = repository.Database.GetCollection<ProductDTO>("Products");
    }
    public async Task Delete(string id)
    {
        var filter = Builders<ProductDTO>
            .Filter
            .Eq(x => x.ProductId, new ObjectId(id));

        await Collection.DeleteOneAsync(filter);
    }

    public async Task<List<ProductDTO>> GetAll()
    {
        return await Collection
            .FindAsync(new BsonDocument())
            .Result.ToListAsync();
    }

    public async Task<ProductDTO> GetById(string id)
    {
        return await Collection
            .FindAsync(new BsonDocument { { "_id", new ObjectId(id) } })
            .Result.FirstAsync();
    }

    public async Task Insert(ProductDTO product)
    {
        await Collection.InsertOneAsync(product);
    }

    public async Task Update(ProductDTO product)
    {
        var filter = Builders<ProductDTO>
            .Filter
            .Eq(x => x.ProductId, product.ProductId);

        await Collection.ReplaceOneAsync(filter, product);
    }
}
