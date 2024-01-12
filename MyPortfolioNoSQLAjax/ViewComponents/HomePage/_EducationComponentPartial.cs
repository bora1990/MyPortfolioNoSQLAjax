using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyPortfolioNoSQLAjax.DAL.Entities;
using MyPortfolioNoSQLAjax.DAL.Settings;

namespace MyPortfolioNoSQLAjax.ViewComponents.HomePage
{
    public class _EducationComponentPartial:ViewComponent
    {
        private readonly IMongoCollection<Education> _educationCollection;
        public _EducationComponentPartial(IDataBaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DataBaseName);
            _educationCollection = database.GetCollection<Education>(_databaseSettings.EducationCollectionName);
        }

        public IViewComponentResult Invoke()
        {
            var values=_educationCollection.Find(x=>true).ToList();

            return View(values);
        }

    }
}
