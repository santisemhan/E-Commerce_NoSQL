namespace Commerce.Core.DataTransferObjects
{
    using Cart.Core.DataTransferObjects;

    public class PaymentDTO
    {
        public string PaymentId { get; set; }

        public string OrderId { get; set; }

        public UserDTO User { get; set; }

        public DateTime TimeStamp { get; set; }

        public string PaymentType { get; set; }
    }
}
