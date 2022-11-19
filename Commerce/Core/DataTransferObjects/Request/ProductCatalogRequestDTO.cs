namespace Commerce.Core.DataTransferObjects.Request
{
    public class ProductCatalogRequestDTO
    {
        public Guid ProductId { get; set; }

        public decimal Price { get; set; }
    }
}
