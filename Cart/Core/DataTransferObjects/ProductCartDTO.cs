namespace Cart.Core.DataTransferObjects
{
    public class ProductCartDTO
    {
        public string ProductCatalogId { get; set; }

        public string ProductName { get; set; }

        public string ImageURL { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}
