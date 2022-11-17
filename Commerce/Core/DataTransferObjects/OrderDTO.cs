namespace Commerce.Core.DataTransferObjects
{
    using Cart.Core.DataTransferObjects;
    using MongoDB.Bson;

    public class OrderDTO
    {
        public ObjectId OrderId { get; set; }
        public Guid OrderIdGuid { get; set; }

        public DateTime TimeStamp { get; set; }

        public UserDTO User { get; set; }

        public List<ProductCartDTO> Products { get; set; }

        public bool IVA { get; set; }

        public decimal FinalPrice { get; set; }
    }
}
