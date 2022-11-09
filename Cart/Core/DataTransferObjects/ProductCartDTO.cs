namespace Cart.Core.DataTransferObjects
{
    using StackExchange.Redis;

    public class ProductCartDTO
    {
        public Guid ProductCatalogId { get; set; }

        public string ProductName { get; set; }

        public string ImageURL { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public ProductCartDTO() { }

        public HashEntry[] ToHashEntries()
        {
            return new HashEntry[]
           {
                new HashEntry(nameof(ProductName), ProductName),
                new HashEntry(nameof(ImageURL), ImageURL),
                new HashEntry(nameof(Quantity), Quantity),
                new HashEntry(nameof(Price), Price),
           };
        }

        public void AddHashEntryData(Guid id, HashEntry[] entries)
        {
            ProductCatalogId = id;
            foreach (var entry in entries)
            {
                var value = entry.Value;
                switch (entry.Name)
                {
                    case nameof(ProductName):
                        ProductName = value;
                        break;
                    case nameof(ImageURL):
                        ImageURL = value;
                        break;
                    case nameof(Quantity):
                        Quantity = int.Parse(value);
                        break;
                    case nameof(Price):
                        Price = double.Parse(value);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
