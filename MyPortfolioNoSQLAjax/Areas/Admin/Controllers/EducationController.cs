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
    public class EducationController : Controller
    {

        private readonly IMongoCollection<Education> _educationCollection;
        public EducationController(IDataBaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DataBaseName);
            _educationCollection = database.GetCollection<Education>(_databaseSettings.EducationCollectionName);
        }
        public async Task<IActionResult> Index()
        {
            var viewModel = new EducationViewModel
            {
                Items = _educationCollection.Find(x => true).ToList()
     .Select(item => new SelectListItem
     {
         Value = item.EducationID.ToString(), // MongoDB ObjectId'yi stringe çevir
         Text = item.Title // Görüntülenen metin
     })
     .ToList()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> EducationList()
        {
            var values = await _educationCollection.Find(x => true).ToListAsync();
            var jsonEducation = JsonSerializer.Serialize(values);
            return Json(jsonEducation);
        }


        [HttpPost]
        public async Task<IActionResult> CreateEducation(Education education)
        {
            await _educationCollection.InsertOneAsync(education);
            var values = JsonSerializer.Serialize(education);
            return Json(values);
        }

        public async Task<IActionResult> GetEducation(string EducationId)
        {
            var values = await _educationCollection.Find(x => x.EducationID == EducationId).FirstOrDefaultAsync();
            var jsonvalues = JsonSerializer.Serialize(values);
            return Json(jsonvalues);
        }

        public async Task<IActionResult> DeleteEducation(string id)
        {
            await _educationCollection.DeleteOneAsync(x => x.EducationID == id);
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEducation(Education education)
        {
            var values = await _educationCollection.FindOneAndReplaceAsync(x => x.EducationID == education.EducationID, education);
            return NoContent();

        }
    }
}
