namespace Commerce.Core.DataTransferObjects.Request
{
    public class ProductCartRequestDTO
    {
        public Guid ProductCatalogId { get; set; }

        public int Quantity { get; set; }
    }
}
