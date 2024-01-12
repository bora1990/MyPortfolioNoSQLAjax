using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using MongoDB.Driver;
using MyPortfolioNoSQLAjax.DAL.Entities;
using MyPortfolioNoSQLAjax.DAL.Settings;

namespace MyPortfolioNoSQLAjax.ViewComponents.HomePage
{
    public class _ProjectComponentPartial:ViewComponent
    {
        private readonly IMongoCollection<Project> _projectCollection;
        public _ProjectComponentPartial(IDataBaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DataBaseName);
            _projectCollection = database.GetCollection<Project>(_databaseSettings.ProjectCollectionName);

        }

        public IViewComponentResult Invoke()
        {
            
            ViewBag.v1=_projectCollection.Find(x=>true).ToList().OrderBy(x=>x.ProjectID).Take(3);
            ViewBag.v2=_projectCollection.Find(x=>true).ToList().OrderByDescending(x=>x.ProjectID).Take(3);

            return View();

        }
    }
}
