namespace Cart.Core.DataTransferObjects
{
    using StackExchange.Redis;

    public class UserCartDTO
    {
        public UserDTO User { get; set; }

        public List<ProductCartDTO> Products { get; set; }

        public UserCartDTO() { }

        public UserCartDTO(HashEntry[] entries) 
        { 

        }

        public HashEntry[] ToHashEntries()
        {
            var entries = new HashEntry[50];
            entries.Append(new HashEntry(nameof(User.UserId), User.UserId));

            return entries;
        }
    }
}
