using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MyPortfolioNoSQLAjax.Areas.Admin.Models
{
    public class ServiceViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ServiceID { get; set; }
        public string Title { get; set; }
        public List<SelectListItem> Items { get; set; }
    }
}
