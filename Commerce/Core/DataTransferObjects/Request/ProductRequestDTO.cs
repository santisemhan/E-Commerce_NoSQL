namespace Commerce.Core.DataTransferObjects.Request
{
    public class ProductRequestDTO
    {
        public string ProductName { get; set; }

        public List<string> ImagesURL { get; set; }

        public string MainImage { get; set; }

        public string Description { get; set; }

        public List<string> Comments { get; set; }

        public int Stock { get; set; }
    }
}
