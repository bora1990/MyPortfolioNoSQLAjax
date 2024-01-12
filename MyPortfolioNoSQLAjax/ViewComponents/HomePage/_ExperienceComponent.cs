using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyPortfolioNoSQLAjax.DAL.Entities;
using MyPortfolioNoSQLAjax.DAL.Settings;

namespace MyPortfolioNoSQLAjax.ViewComponents.HomePage
{
    public class _ExperienceComponent:ViewComponent
    {
        private readonly IMongoCollection<Experience> _experienceCollection;
        public _ExperienceComponent(IDataBaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DataBaseName);
            _experienceCollection = database.GetCollection<Experience>(_databaseSettings.ExperienceCollectionName);
        }
        public IViewComponentResult Invoke()
        {
            var values= _experienceCollection.Find(x=>true).ToList();
            return View(values);
        }

            

    }
}
