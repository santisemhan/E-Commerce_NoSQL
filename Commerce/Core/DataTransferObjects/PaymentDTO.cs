namespace Commerce.Core.DataTransferObjects
{
    using Cart.Core.DataTransferObjects;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class PaymentDTO
    {
        [BsonId]
        public ObjectId PaymentId { get; set; }

        public string OrderId { get; set; }

        public UserDTO User { get; set; }

        public DateTime TimeStamp { get; set; }

        public string PaymentType { get; set; }
    }
}
