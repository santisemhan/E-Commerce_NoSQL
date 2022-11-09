namespace Cart.Core.DataTransferObjects
{
    using StackExchange.Redis;

    public class UserDTO
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Adress { get; set; }

        public HashEntry[] ToHashEntries()
        {
            return new HashEntry[]
            {
                new HashEntry(nameof(Name), Name),
                new HashEntry(nameof(LastName), LastName),
                new HashEntry(nameof(Adress), Adress)
            };
        }

        public void AddHashEntryData(Guid id, HashEntry[] entries)
        {
            UserId = id;
            foreach (var entry in entries)
            {
                var value = entry.Value.ToString();
                switch (entry.Name)
                {
                    case nameof(Name):
                        Name = value;
                        break;
                    case nameof(LastName):
                        LastName = value;
                        break;
                    case nameof(Adress):
                        Adress = value;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
