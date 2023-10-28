using DoReMusic.MVC.Models;
using DoReMusic.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using DoReMusic.Domain.Entities;
using DoReMusic.MVC.ViewModels;
using System.Drawing;
using DoReMusic.Domain.Enum;
using Color = DoReMusic.Domain.Enum.Color;

namespace DoReMusic.MVC.Controllers
{
    public class InstrumentsController : Controller
    {
        private readonly DoReMusicDbContext doReMusicDbContext;

        public InstrumentsController()
        {
            doReMusicDbContext = new DoReMusicDbContext();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddInstrument()
        {
            InstrumentViewModel instrumentViewModel = new()
            {
                Brands = doReMusicDbContext.Brands.ToList(),
                Categories = doReMusicDbContext.Categories.ToList(),
            };
            return View(instrumentViewModel);
        }

        [HttpPost]
        public IActionResult AddInstrument(AddInstrumentRequest InstrumentRequest)
        {
            Brand tempBrand = doReMusicDbContext.Brands.Where(x => x.Id == Guid.Parse(InstrumentRequest.Brand)).FirstOrDefault();
            Category tempCategory = doReMusicDbContext.Categories.Where(x => x.Id == Guid.Parse(InstrumentRequest.Category)).FirstOrDefault();

            //if (tempBrand == null || tempCategory == null) return NotFound();
            
            
            Instrument NewInstrument = new Instrument()
            {
                Id = Guid.NewGuid(),
                Name = InstrumentRequest.Name,
                Brand = tempBrand,
                Category = tempCategory,
                Kind = InstrumentRequest.Kind,
                Model = InstrumentRequest.Model,
                Color = (Color) Convert.ToInt32(InstrumentRequest.Color),
                Price = InstrumentRequest.Price,
                ProductionYear = InstrumentRequest.ProductionYear,
            };

            doReMusicDbContext.Instruments.Add(NewInstrument);
            doReMusicDbContext.SaveChanges();


            return RedirectToAction("AddInstrument");
        }

        public IActionResult InstrumentsOfKind(string category, string kind)
        {
            // Retrieve instruments that match the selected category and kind
            // You can fetch data from your data source based on the category and kind
            var instruments = doReMusicDbContext.Instruments.Where(x => x.Category.Name == category && x.Kind == kind).ToList();

            // Pass the list of instruments to the view
            return View(instruments);
        }

    }
}
