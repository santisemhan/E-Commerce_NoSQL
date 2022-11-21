namespace Commerce.Core.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid ProductId { get; set; } = Guid.NewGuid();

        public string ProductName { get; set; }
      
        public List<string> ImagesURL { get; set; }

        public string MainImage { get; set; }

        public string Description { get; set; }

        public List<string> Comments { get; set; }

        public int Stock { get; set; }
    }
}
