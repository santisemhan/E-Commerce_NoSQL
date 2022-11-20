namespace Commerce.Core.DataTransferObjects.Request
{
    public class OrderDTO
    {
        public Guid idUser { get; set; }
        public List<ProductCartDTO> Products { get; set; }
        public bool IVA { get; set; }
    }
}
