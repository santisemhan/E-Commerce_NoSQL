using Commerce.Core.DataTransferObjects;
using Commerce.Core.Repositories.Contexts.Interfaces;
using Commerce.Core.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Commerce.Core.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly IConnection<IMongoDatabase> _mongoConnection;

    public PaymentRepository(IConnection<IMongoDatabase> mongoConnection)
    {
        _mongoConnection = mongoConnection;
    }

    public async Task<List<PaymentDTO>> GetAll()
    {
        return (await _mongoConnection.GetConnection()
            .GetCollection<PaymentDTO>("Payments")
            .FindAsync(new BsonDocument()))
            .ToList();
    }

    public async Task<PaymentDTO> GetById(string id)
    {
        return (await _mongoConnection.GetConnection()
            .GetCollection<PaymentDTO>("Payments")
            .FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }))
            .First();
    }

    public async Task Insert(PaymentDTO newPayment)
    {
        await _mongoConnection.GetConnection()
            .GetCollection<PaymentDTO>("Products")
            .InsertOneAsync(newPayment);
    }
}
