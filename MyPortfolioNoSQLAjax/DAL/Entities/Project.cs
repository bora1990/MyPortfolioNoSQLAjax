using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MyPortfolioNoSQLAjax.DAL.Entities
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProjectID { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
    }
}
