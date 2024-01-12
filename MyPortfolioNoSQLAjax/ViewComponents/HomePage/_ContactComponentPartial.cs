using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyPortfolioNoSQLAjax.DAL.Entities;
using MyPortfolioNoSQLAjax.DAL.Settings;

namespace MyPortfolioNoSQLAjax.ViewComponents.HomePage
{
    public class _ContactComponentPartial:ViewComponent
    {
        private readonly IMongoCollection<Contact> _contactCollection;
        public _ContactComponentPartial(IDataBaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DataBaseName);
            _contactCollection = database.GetCollection<Contact>(_databaseSettings.ContactCollectionName);
        }

        public IViewComponentResult Invoke()
        {
            var value = _contactCollection.Find(x => true).FirstOrDefault();

            return View(value);
        }
    }
}
