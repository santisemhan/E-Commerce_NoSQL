namespace Commerce.Core.DataTransferObjects
{
    using Cart.Core.DataTransferObjects;

    public class OrderDTO
    {
        public string OrderId { get; set; }

        public DateTime TimeStamp { get; set; }

        public UserDTO User { get; set; }

        public List<ProductCartDTO> Products { get; set; }

        public bool IVA { get; set; }

        public decimal FinalPrice { get; set; }
    }
}
