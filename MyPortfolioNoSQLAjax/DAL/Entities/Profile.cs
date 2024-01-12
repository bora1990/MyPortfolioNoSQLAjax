using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MyPortfolioNoSQLAjax.DAL.Entities
{
    public class Profile
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProfileID { get; set; }
        public string NameSurname { get; set; }
        public string ImageUrl { get; set; }
        public string Title1 { get; set; }
        public string Title2 { get; set; }
      
    }
}
