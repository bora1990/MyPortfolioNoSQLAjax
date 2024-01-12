using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyPortfolioNoSQLAjax.DAL.Entities;
using MyPortfolioNoSQLAjax.DAL.Settings;

namespace MyPortfolioNoSQLAjax.ViewComponents.HomePage
{
    public class _SkillComponentPartial:ViewComponent
    {
        private readonly IMongoCollection<Skill> _skillCollection;
        public _SkillComponentPartial(IDataBaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DataBaseName);
            _skillCollection = database.GetCollection<Skill>(_databaseSettings.SkillCollectionName);

        }

        public IViewComponentResult Invoke()
        {
            ViewBag.skill1 = _skillCollection.Find(x => true).ToList().OrderBy(x=>x.Title).Take(3);

            ViewBag.skill2 = _skillCollection.Find(x => true).ToList().OrderByDescending(x=>x.Title).Take(3);

            return View();
        }

    }
}
