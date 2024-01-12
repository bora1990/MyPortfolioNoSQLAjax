using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MyPortfolioNoSQLAjax.DAL.Entities
{
    public class Education
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string EducationID { get; set; }
        public string Title { get; set; }
        public string Time { get; set; }
        public string School { get; set; }
    }
}
