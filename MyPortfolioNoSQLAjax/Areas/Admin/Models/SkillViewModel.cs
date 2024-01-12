using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyPortfolioNoSQLAjax.Areas.Admin.Models
{
    public class SkillViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SkillID { get; set; }
        public string Title { get; set; }
        public List<SelectListItem> Items { get; set; }
    }
}
