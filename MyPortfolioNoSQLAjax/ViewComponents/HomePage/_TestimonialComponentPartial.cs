using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyPortfolioNoSQLAjax.DAL.Entities;
using MyPortfolioNoSQLAjax.DAL.Settings;

namespace MyPortfolioNoSQLAjax.ViewComponents.HomePage
{
    public class _TestimonialComponentPartial:ViewComponent
    {
        private readonly IMongoCollection<Testimonial> _testimonialCollection;
        public _TestimonialComponentPartial(IDataBaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DataBaseName);
            _testimonialCollection = database.GetCollection<Testimonial>(_databaseSettings.TestimonialCollectionName);

        }

        public IViewComponentResult Invoke()
        {
            var values=_testimonialCollection.Find(x=>true).ToList();

            return View(values);
        }


    }
}
