namespace Commerce.Core.DataTransferObjects.Request
{
    public class ProductCartDTO
    {
        public Guid ProductCatalogId { get; set; }

        public int Quantity { get; set; }
    }
}
