using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyPortfolioNoSQLAjax.Areas.Admin.Models
{
    public class ExperienceViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ExperienceID { get; set; }
        public string Title { get; set; }
        public List<SelectListItem> Items { get; set; }
    }
}
