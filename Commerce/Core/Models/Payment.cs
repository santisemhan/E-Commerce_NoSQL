using MongoDB.Bson.Serialization.Attributes;

namespace Commerce.Core.Models;

public class Payment
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid PaymentId { get; set; } = Guid.NewGuid();

    public Guid OrderId { get; set; }

    public User User { get; set; }

    public DateTime TimeStamp { get; set; }

    public string PaymentType { get; set; }
}
