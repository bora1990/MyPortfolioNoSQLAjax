using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Driver;
using MyPortfolioNoSQLAjax.Areas.Admin.Models;
using MyPortfolioNoSQLAjax.DAL.Entities;
using MyPortfolioNoSQLAjax.DAL.Settings;
using Newtonsoft.Json;

namespace MyPortfolioNoSQLAjax.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class ProfileController : Controller
    {
        private readonly IMongoCollection<Profile> _profileCollection;
        public ProfileController(IDataBaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DataBaseName);
            _profileCollection = database.GetCollection<Profile>(_databaseSettings.ProfileCollectionName);

        }
        public IActionResult Index()
        {
            var viewModel = new ProfileViewModel
            {
                Items = _profileCollection.Find(x => true).ToList()
        .Select(item => new SelectListItem
        {
            Value = item.ProfileID.ToString(), // MongoDB ObjectId'yi stringe çevir
            Text = item.NameSurname // Görüntülenen metin
        })
        .ToList()
            };


            return View(viewModel);
        }

        public async Task<IActionResult> ProfileList()
        {
            var values = await _profileCollection.Find(x => true).ToListAsync();
            var jsonProfile = JsonConvert.SerializeObject(values);
            return Json(jsonProfile);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProfile(Profile profile)
        {
            await _profileCollection.InsertOneAsync(profile);
            var values = JsonConvert.SerializeObject(profile);
            return Json(values);
        }

        public async Task<IActionResult> GetProfile(string profileId)
        {
            var values = await _profileCollection.Find(x => x.ProfileID == profileId).FirstOrDefaultAsync();
            var jsonvalues = JsonConvert.SerializeObject(values);
            return Json(jsonvalues);
        }

        public async Task<IActionResult> DeleteProfile(string id)
        {
            await _profileCollection.DeleteOneAsync(x => x.ProfileID == id);
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(Profile profile)
        {
            var values = await _profileCollection.FindOneAndReplaceAsync(x => x.ProfileID == profile.ProfileID, profile);
            return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> ProfileDetail(string id)
        {
            var values = await _profileCollection.Find(x => x.ProfileID == id).FirstOrDefaultAsync();
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> ProfileDetail(Profile profile)
        {
            await _profileCollection.FindOneAndReplaceAsync(x => x.ProfileID == profile.ProfileID, profile);
            return RedirectToAction("Index");
        }
    }
}
