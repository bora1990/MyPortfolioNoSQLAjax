using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyPortfolioNoSQLAjax.DAL.Entities;
using MyPortfolioNoSQLAjax.DAL.Settings;

namespace MyPortfolioNoSQLAjax.ViewComponents.HomePage
{
    public class _ServiceComponentPartial:ViewComponent
    {
        private readonly IMongoCollection<Service> _serviceCollection;
        public _ServiceComponentPartial(IDataBaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DataBaseName);
            _serviceCollection = database.GetCollection<Service>(_databaseSettings.ServiceCollectionName);
        }

        public IViewComponentResult Invoke()
        {
            var values=_serviceCollection.Find(x=>true).ToList();

            return View(values);
        }

    }
}
