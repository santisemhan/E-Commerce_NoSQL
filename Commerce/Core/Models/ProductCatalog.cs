namespace Commerce.Core.Models
{
    using Cassandra;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class ProductCatalog
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ProductId { get; set; }

        public decimal Price { get; set; }

        public ProductCatalog() { }

        public ProductCatalog(Row row)
        {
            Id = row.GetValue<Guid>(nameof(Id).ToLower());
            ProductId = row.GetValue<Guid>(nameof(ProductId).ToLower());
            Price = row.GetValue<decimal>(nameof(Price).ToLower());
        }
    }
}
