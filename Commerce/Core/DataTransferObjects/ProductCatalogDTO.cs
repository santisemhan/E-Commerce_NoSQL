using Cassandra;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Commerce.Core.DataTransferObjects
{
    public class ProductCatalogDTO
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid ProductCatalogId { get; set; }

        public Guid ProductId { get; set; }

        public decimal Price { get; set; }

        public ProductCatalogDTO(Row row)
        {
            ProductCatalogId = row.GetValue<Guid>(nameof(ProductCatalogId).ToLower());
            ProductId = row.GetValue<Guid>(nameof(ProductId).ToLower());
            Price = row.GetValue<decimal>(nameof(Price).ToLower());
        }
    }
}
