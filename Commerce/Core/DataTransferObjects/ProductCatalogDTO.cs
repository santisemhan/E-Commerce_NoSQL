namespace Commerce.Core.DataTransferObjects.Request
{
    public class ProductCatalogDTO
    {
        public Guid AuthorId { get; set; }

        public Guid ProductId { get; set; }
        
        public DateTime Moment { get; set; }

        public decimal Price { get; set; }

    }
}
