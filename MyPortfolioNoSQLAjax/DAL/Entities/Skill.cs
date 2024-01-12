using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MyPortfolioNoSQLAjax.DAL.Entities
{
    public class Skill
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SkillID { get; set; }
        public string Title { get; set; }
        public string Points { get; set; }
    }
}
