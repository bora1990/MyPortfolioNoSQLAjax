using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Driver;
using MyPortfolioNoSQLAjax.Areas.Admin.Models;
using MyPortfolioNoSQLAjax.DAL.Entities;
using MyPortfolioNoSQLAjax.DAL.Settings;
using System.Text.Json;

namespace MyPortfolioNoSQLAjax.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class SkillController : Controller
    {
        private readonly IMongoCollection<Skill> _skillCollection;
        public SkillController(IDataBaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DataBaseName);
            _skillCollection = database.GetCollection<Skill>(_databaseSettings.SkillCollectionName);
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new SkillViewModel
            {
                Items = _skillCollection.Find(x => true).ToList()
     .Select(item => new SelectListItem
     {
         Value = item.SkillID.ToString(), // MongoDB ObjectId'yi stringe çevir
         Text = item.Title // Görüntülenen metin
     })
     .ToList()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> SkillList()
        {
            var values = await _skillCollection.Find(x => true).ToListAsync();
            var jsonSkill = JsonSerializer.Serialize(values);
            return Json(jsonSkill);
        }


        [HttpPost]
        public async Task<IActionResult> CreateSkill(Skill skill)
        {
            await _skillCollection.InsertOneAsync(skill);
            var values = JsonSerializer.Serialize(skill);
            return Json(values);
        }

        public async Task<IActionResult> GetSkill(string SkillId)
        {
            var values = await _skillCollection.Find(x => x.SkillID == SkillId).FirstOrDefaultAsync();
            var jsonvalues = JsonSerializer.Serialize(values);
            return Json(jsonvalues);
        }

        public async Task<IActionResult> DeleteSkill(string id)
        {
            await _skillCollection.DeleteOneAsync(x => x.SkillID == id);
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSkill(Skill skill)
        {
            var values = await _skillCollection.FindOneAndReplaceAsync(x => x.SkillID == skill.SkillID, skill);
            return NoContent();

        }
    }
}
