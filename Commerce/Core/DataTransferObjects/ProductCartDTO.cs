namespace Commerce.Core.DataTransferObjects
{
    public class ProductCartDTO
    {
        public ProductCatalogDTO ProductCatalog { get; set; }

        public int Quantity { get; set; }
    }
}
