namespace Commerce.Core.DataTransferObjects.Request
{
    public class OrderRequestDTO
    {
        public Guid idUser { get; set; }
        public List<ProductCartRequestDTO> Products { get; set; }
        public bool IVA { get; set; }
    }
}
