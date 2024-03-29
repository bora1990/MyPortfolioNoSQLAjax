﻿using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyPortfolioNoSQLAjax.DAL.Entities;
using MyPortfolioNoSQLAjax.DAL.Settings;

namespace MyPortfolioNoSQLAjax.Areas.Admin.Controllers
{
    [Area("Admin")]  
    [Route("[area]/[controller]/[action]/{id?}")]
    public class AboutController : Controller
    {
        private readonly IMongoCollection<About> _aboutCollection;
        public AboutController(IDataBaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DataBaseName);
            _aboutCollection = database.GetCollection<About>(_databaseSettings.AboutCollectionName);

        }
        public async Task<IActionResult> Index()
        {
            var values = await _aboutCollection.Find(x => true).ToListAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(About about)
        {
            await _aboutCollection.InsertOneAsync(about);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAbout(string id)
        {
            await _aboutCollection.DeleteOneAsync(x => x.AboutID == id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAbout(string id)
        {
            var values = await _aboutCollection.Find(x => x.AboutID == id).FirstOrDefaultAsync();
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(About about)
        {
            await _aboutCollection.FindOneAndReplaceAsync(x => x.AboutID == about.AboutID, about);
            return RedirectToAction("Index");
        }
    }
}
