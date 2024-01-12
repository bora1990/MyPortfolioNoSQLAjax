using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyPortfolioNoSQLAjax.DAL.Entities
{
    public class About
    {
        [BsonId] //Id olduğu
        [BsonRepresentation(BsonType.ObjectId)]  //Türü  MongoDb de_id → objectid(Unique Key)
        public string AboutID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Kind1 { get; set; }
        public string? Kind2 { get; set; }
        public string? Kind3 { get; set; }
    }
}
