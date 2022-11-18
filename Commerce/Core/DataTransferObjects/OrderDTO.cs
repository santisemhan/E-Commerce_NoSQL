namespace Commerce.Core.DataTransferObjects
{
    using MongoDB.Bson.Serialization.Attributes;

    public class OrderDTO
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid OrderId { get; set; }

        public DateTime TimeStamp { get; set; }

        public UserDTO User { get; set; }

        public List<ProductCartDTO> Products { get; set; }

        public bool IVA { get; set; }

        public decimal FinalPrice { get; set; }
    }
}
