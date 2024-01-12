using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyPortfolioNoSQLAjax.Areas.Admin.Models
{
    public class EducationViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string EducationID { get; set; }
        public string Title { get; set; }
        public List<SelectListItem> Items { get; set; }
    }
}
