using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cart.Core.DataTransferObjects
{
    public class UserDTO
    {
        [BsonId]
        public ObjectId UserId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Adress { get; set; }
    }
}
