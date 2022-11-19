namespace Commerce.Core.DataTransferObjects
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class UserDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Adress { get; set; }
    }
}
