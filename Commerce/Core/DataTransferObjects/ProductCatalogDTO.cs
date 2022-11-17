using Cassandra;
using MongoDB.Bson;

namespace Commerce.Core.DataTransferObjects
{
    public class ProductCatalogDTO
    {
        public ObjectId ProductCatalogId { get; set; }
        public Guid ProductCatalogIdGuid { get; set; }

        public string ProductId { get; set; }

        public decimal Price { get; set; }

        public ProductCatalogDTO(Row row)
        {
            ProductCatalogId = row.GetValue<ObjectId>(nameof(ProductCatalogId).ToLower());
            ProductCatalogIdGuid = row.GetValue<Guid>(nameof(ProductCatalogIdGuid).ToLower());
            ProductId = row.GetValue<string>(nameof(ProductId).ToLower());
            Price = row.GetValue<decimal>(nameof(Price).ToLower());
        }
    }
}
