﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Driver;
using MyPortfolioNoSQLAjax.Areas.Admin.Models;
using MyPortfolioNoSQLAjax.DAL.Entities;
using MyPortfolioNoSQLAjax.DAL.Settings;
using Newtonsoft.Json;
using System.Text.Json;


namespace MyPortfolioNoSQLAjax.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class ServiceController : Controller
    {
        private readonly IMongoCollection<Service> _servicesCollection;
        public ServiceController(IDataBaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DataBaseName);
            _servicesCollection = database.GetCollection<Service>(_databaseSettings.ServiceCollectionName);
        }
        public IActionResult Index()
        {
            var viewModel = new ServiceViewModel
            {
                Items = _servicesCollection.Find(x => true).ToList()
        .Select(item => new SelectListItem
        {
            Value = item.ServiceID.ToString(), // MongoDB ObjectId'yi stringe çevir
            Text = item.Title // Görüntülenen metin
        })
        .ToList()
            };


            return View(viewModel);
        }

        public async Task<IActionResult> ServiceList()
        {
            var values = await _servicesCollection.Find(x => true).ToListAsync();
            var jsonService = JsonConvert.SerializeObject(values);
            return Json(jsonService);
        }

    
        [HttpPost]
        public async Task<IActionResult> CreateService(Service service)
        {
            await _servicesCollection.InsertOneAsync(service);
            var values = JsonConvert.SerializeObject(service);
            return Json(values);
        }

        public async Task<IActionResult> GetService(string ServiceId)
        {
            var values = await _servicesCollection.Find(x => x.ServiceID == ServiceId).FirstOrDefaultAsync();
            var jsonvalues = JsonConvert.SerializeObject(values);
            return Json(jsonvalues);
        }

        public async Task<IActionResult> DeleteService(string id)
        {
            await _servicesCollection.DeleteOneAsync(x => x.ServiceID == id);
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateService(Service service)
        {
            var values = await _servicesCollection.FindOneAndReplaceAsync(x => x.ServiceID == service.ServiceID, service);
            return NoContent();

        }
    }
}
