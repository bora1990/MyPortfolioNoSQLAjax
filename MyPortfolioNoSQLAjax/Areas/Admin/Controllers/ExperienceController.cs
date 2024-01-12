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
    public class ExperienceController : Controller
    {
        private readonly IMongoCollection<Experience> _experienceCollection;
        public ExperienceController(IDataBaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DataBaseName);
            _experienceCollection = database.GetCollection<Experience>(_databaseSettings.ExperienceCollectionName);
        }
        public async Task<IActionResult> Index()
        {
            var viewModel = new ExperienceViewModel
            {
                Items = _experienceCollection.Find(x => true).ToList()
     .Select(item => new SelectListItem
     {
         Value = item.ExperienceID.ToString(), // MongoDB ObjectId'yi stringe çevir
         Text = item.Title // Görüntülenen metin
     })
     .ToList()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ExperienceList()
        {
            var values = await _experienceCollection.Find(x => true).ToListAsync();
            var jsonExperience = JsonSerializer.Serialize(values);
            return Json(jsonExperience);
        }


        [HttpPost]
        public async Task<IActionResult> CreateExperience(Experience experience)
        {
            await _experienceCollection.InsertOneAsync(experience);
            var values = JsonSerializer.Serialize(experience);
            return Json(values);
        }

        public async Task<IActionResult> GetExperience(string ExperienceId)
        {
            var values = await _experienceCollection.Find(x => x.ExperienceID == ExperienceId).FirstOrDefaultAsync();
            var jsonvalues = JsonSerializer.Serialize(values);
            return Json(jsonvalues);
        }

        public async Task<IActionResult> DeleteExperience(string id)
        {
            await _experienceCollection.DeleteOneAsync(x => x.ExperienceID == id);
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateExperience(Experience experience)
        {
            var values = await _experienceCollection.FindOneAndReplaceAsync(x => x.ExperienceID == experience.ExperienceID, experience);
            return NoContent();

        }
    }
}
