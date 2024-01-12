using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyPortfolioNoSQLAjax.DAL.Entities;
using MyPortfolioNoSQLAjax.DAL.Settings;


namespace MyPortfolioNoSQLAjax.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]

    public class MessageController : Controller
    {
        private readonly IMongoCollection<Message> _messageCollection;
        public MessageController(IDataBaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DataBaseName);
            _messageCollection = database.GetCollection<Message>(_databaseSettings.MessageCollectionName);

        }
        public async Task<IActionResult> Index()
        {
            var values = await _messageCollection.Find(x => true).ToListAsync();  //mongo kolksiyondaki tüm belgeler HERHANGİ BİR FİLTRELEME YOK
            return View(values);
        }
        [HttpPost]



        [HttpPost]

        public async Task<IActionResult> AddMessage(Message message)
        {
            await _messageCollection.InsertOneAsync(message);   

            return RedirectToAction("Index");
    }



    }
}
