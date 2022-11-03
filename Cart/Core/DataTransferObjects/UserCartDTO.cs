namespace Cart.Core.DataTransferObjects
{
    using StackExchange.Redis;

    public class UserCartDTO
    {
        public string UserId { get; set; }

        // Agregar mas campos del carrito

        public UserCartDTO() { }

        public UserCartDTO(HashEntry[] entries) 
        { 

        }

        public HashEntry[] ToHashEntries()
        {
            var entries = new HashEntry[50];
            entries.Append(new HashEntry(nameof(UserId), UserId));

            return entries;
        }
    }
}
