using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyPortfolioNoSQLAjax.Controllers;
using MyPortfolioNoSQLAjax.DAL.Entities;
using MyPortfolioNoSQLAjax.DAL.Settings;

namespace MyPortfolioNoSQLAjax.ViewComponents.HomePage
{
    public class _HeaderComponentPartial:ViewComponent
    {
        private readonly IMongoCollection<Profile> _profileCollection;

        public _HeaderComponentPartial(IDataBaseSettings _databaseSettings)
        {
           
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DataBaseName);
            _profileCollection = database.GetCollection<Profile>(_databaseSettings.ProfileCollectionName);
        }

        public IViewComponentResult Invoke()
        {
            var values=_profileCollection.Find(x=>true).FirstOrDefault();
            return View(values);
        }

       
    }
}
