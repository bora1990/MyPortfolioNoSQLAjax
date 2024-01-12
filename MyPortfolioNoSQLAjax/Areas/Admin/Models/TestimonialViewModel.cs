using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyPortfolioNoSQLAjax.Areas.Admin.Models
{
    public class TestimonialViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TestimonialID { get; set; }
        public string NameSurname { get; set; }
        public List<SelectListItem> Items { get; set; }
    }
}
