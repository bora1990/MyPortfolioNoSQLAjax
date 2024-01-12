using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using MyPortfolioNoSQLAjax.Areas.Admin.Models;
using MyPortfolioNoSQLAjax.DAL.Entities;
using MyPortfolioNoSQLAjax.DAL.Settings;
using System.Text.Json;

namespace MyPortfolioNoSQLAjax.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class TestimonialController : Controller
    {
        private readonly IMongoCollection<Testimonial> _testimonialCollection;
        public TestimonialController(IDataBaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DataBaseName);
            _testimonialCollection = database.GetCollection<Testimonial>(_databaseSettings.TestimonialCollectionName);
        }
        public IActionResult Index()
        {
            var viewModel = new TestimonialViewModel
            {
                Items = _testimonialCollection.Find(x => true).ToList()
    .Select(item => new SelectListItem
    {
        Value = item.TestimonialID.ToString(), // MongoDB ObjectId'yi stringe çevir
        Text = item.NameSurname // Görüntülenen metin
    })
    .ToList()
            };


            return View(viewModel);
        }  
        
        public async Task<IActionResult> TestimonialList()
        {
            var values = await _testimonialCollection.Find(x => true).ToListAsync();
            var jsonTest=JsonSerializer.Serialize(values);

            return Json(jsonTest);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(Testimonial testimonial)
        {
            await _testimonialCollection.InsertOneAsync(testimonial);
            var values = JsonSerializer.Serialize(testimonial);
            return Json(values);

        }

        public async Task<IActionResult> GetTestimonial(string testimonialID)
        {
            var values = await _testimonialCollection.Find(x => x.TestimonialID == testimonialID).FirstOrDefaultAsync();

            return Json(values);
        }

        public async Task<IActionResult> DeleteTestimonial(string id)
        {
            await _testimonialCollection.DeleteOneAsync(x => x.TestimonialID == id);
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(Testimonial testimonial)
        {
            var values = await _testimonialCollection.FindOneAndReplaceAsync(x => x.TestimonialID == testimonial.TestimonialID, testimonial);
            return NoContent();

        }

        public async Task<IActionResult> TestimonialDetail(string id)
        {
            var values = await _testimonialCollection.Find(x => x.TestimonialID == id).FirstOrDefaultAsync();
            return View(values);
        }
    }
}
