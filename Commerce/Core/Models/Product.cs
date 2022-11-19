using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Commerce.Core.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid ProductId { get; set; } = Guid.NewGuid();

        public string ProductName { get; set; }
      
        public List<string> ImagesURL { get; set; }

        public string MainImage { get; set; }

        public string Description { get; set; }

        public List<string> Comments { get; set; }

        public int Stock { get; set; }
    }
}
