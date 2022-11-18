using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Commerce.Core.DataTransferObjects
{
    public class UserDTO
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Adress { get; set; }
    }

    /*OBS: ¿Con que formato se guarda el id en mongo?
     
        SIN BsonRepresentation
            _id: BinData(3, 'ZF+oPxdXYkWz/CyWP2avpg==')
        
        CON BsonRepresentation
        _id:"3fa85f64-5717-4562-b3fc-2c963f66afa6"
    */

}
