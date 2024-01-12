using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyPortfolioNoSQLAjax.DAL.Entities;
using MyPortfolioNoSQLAjax.DAL.Settings;

namespace MyPortfolioNoSQLAjax.ViewComponents.HomePage
{
    public class _AboutComponentPartial : ViewComponent
    {
        private readonly IMongoCollection<About> _aboutCollection;
        public _AboutComponentPartial(IDataBaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DataBaseName);
            _aboutCollection = database.GetCollection<About>(_databaseSettings.AboutCollectionName);

        }

        public IViewComponentResult Invoke()
        {
          var values = _aboutCollection.Find(x => true).FirstOrDefault();
           
         
            return View(values);
        }

    }
}
