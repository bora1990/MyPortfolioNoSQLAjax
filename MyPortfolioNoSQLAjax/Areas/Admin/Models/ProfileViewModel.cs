using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyPortfolioNoSQLAjax.Areas.Admin.Models
{
    public class ProfileViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProfileID { get; set; }
        public string NameSurname { get; set; }
        public List<SelectListItem> Items { get; set; }
    }
}
