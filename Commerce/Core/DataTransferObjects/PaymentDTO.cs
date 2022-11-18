using MongoDB.Bson.Serialization.Attributes;

namespace Commerce.Core.DataTransferObjects;


public class PaymentDTO
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid PaymentId { get; set; }

    public Guid OrderId { get; set; }

    public UserDTO User { get; set; }

    public DateTime TimeStamp { get; set; }

    public string PaymentType { get; set; }
}
