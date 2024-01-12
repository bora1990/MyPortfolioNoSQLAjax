using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyPortfolioNoSQLAjax.DAL.Entities;
using MyPortfolioNoSQLAjax.DAL.Settings;

namespace MyPortfolioNoSQLAjax.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class ContactController : Controller
    {
        private readonly IMongoCollection<Contact> _ContactCollection;

        public ContactController(IDataBaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DataBaseName);
            _ContactCollection = database.GetCollection<Contact>(_databaseSettings.ContactCollectionName);

        }

        public async Task<IActionResult> Index()
        {
            var values = await _ContactCollection.Find(x => true).ToListAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateContact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(Contact Contact)
        {
            await _ContactCollection.InsertOneAsync(Contact);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteContact(string id)
        {
            await _ContactCollection.DeleteOneAsync(x => x.ContactID == id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateContact(string id)
        {
            var values = await _ContactCollection.Find(x => x.ContactID == id).FirstOrDefaultAsync();
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContact(Contact Contact)
        {
            await _ContactCollection.FindOneAndReplaceAsync(x => x.ContactID == Contact.ContactID, Contact);
            return RedirectToAction("Index");
        }
    }
}
