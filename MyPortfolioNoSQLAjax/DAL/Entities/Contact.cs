using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MyPortfolioNoSQLAjax.DAL.Entities
{
    public class Contact
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ContactID { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }

    }
}
