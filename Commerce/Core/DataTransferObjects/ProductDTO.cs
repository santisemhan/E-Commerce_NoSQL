using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Commerce.Core.DataTransferObjects
{
    public class ProductDTO
    {
        [BsonId]
        public ObjectId ProductId { get; set; }

        public string ProductName { get; set; }
      
        public List<string> ImagesURL { get; set; }

        public string MainImage { get; set; }

        public string Description { get; set; }

        public List<string> Comments { get; set; }

        public int Stock { get; set; }
    }
}
