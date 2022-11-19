namespace Commerce.Core.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid UserId { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Adress { get; set; }
    }
}
