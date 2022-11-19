namespace Cart.Core.DataTransferObjects
{
    using Cassandra;

    public class UserActivityDTO
    {
        public Guid Id { get; set; }

        public DateTime Moment { get; set; }

        public Guid UserId { get; set; }

        public string ImageUrl { get; set; }

        public double Price { get;set; }

        public Guid ProductCatalogId { get; set; }

        public string ProductName { get; set; } 

        public int Quantity { get; set; }

        public UserActivityDTO() { }

        public UserActivityDTO(Row row)
        {
            Id = row.GetValue<Guid>(nameof(Id).ToLower());
            Moment = row.GetValue<DateTime>(nameof(Moment).ToLower());
            UserId = row.GetValue<Guid>(nameof(UserId).ToLower());
            ImageUrl = row.GetValue<string>(nameof(ImageUrl).ToLower());
            Price = row.GetValue<double>(nameof(Price).ToLower());
            ProductCatalogId = row.GetValue<Guid>(nameof(ProductCatalogId).ToLower());
            ProductName = row.GetValue<string>(nameof(ProductName).ToLower());
            Quantity = row.GetValue<int>(nameof(Quantity).ToLower());
        }
    }
}
