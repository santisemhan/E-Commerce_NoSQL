namespace Cart.Core.DataTransferObjects
{
    using StackExchange.Redis;

    public class UserCartDTO
    {
        public UserDTO User { get; set; }

        public List<ProductCartDTO> Products { get; set; } = new List<ProductCartDTO>();

        public UserCartDTO() { }

        public void AddUserData(Guid id, HashEntry[] entries)
        {
            User = new UserDTO();
            User.AddHashEntryData(id, entries);
        }   

        public void AddProductData(Guid id, HashEntry[] entries)
        {
            var product = new ProductCartDTO();
            product.AddHashEntryData(id, entries);
            Products.Add(product);
        }
    }
}
