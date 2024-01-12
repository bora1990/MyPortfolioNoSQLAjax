using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MyPortfolioNoSQLAjax.DAL.Entities
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MessageID { get; set; }
        public string NameSurname { get; set; }
        public string Mail { get; set; }
        public string Subject { get; set; }
        public string MessageContent { get; set; }
    }
}
