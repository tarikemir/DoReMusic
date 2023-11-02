using DoReMusic.MVC.Models;
using DoReMusic.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using DoReMusic.Domain.Entities;
using DoReMusic.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Drawing;
using DoReMusic.Domain.Enum;
using Color = DoReMusic.Domain.Enum.Color;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using DoReMusic.Persistence.Migrations;

namespace DoReMusic.MVC.Controllers
{
    public class InstrumentsController : Controller
    {
        private readonly DoReMusicDbContext doReMusicDbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public InstrumentsController(UserManager<IdentityUser> userManager)
        {
            doReMusicDbContext = new DoReMusicDbContext();
            _userManager = userManager;
        }
        public IActionResult Index(string sort = "default")
        {
            var Instruments = doReMusicDbContext.Instruments.Include(x => x.Category).Include(x => x.Brand).ToList();

            if (sort == "alphabetic")
            {
                Instruments = Instruments.OrderBy(x => x.Name).ToList();
            }
            else if (sort == "ascendingbyprice")
            {
                Instruments = Instruments.OrderBy(x => x.Price).ToList();
            }
            else if (sort == "descendingbyprice")
            {
                Instruments = Instruments.OrderByDescending(x => x.Price).ToList();
            }

            return View(Instruments);
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
                Color = (Color)Convert.ToInt32(InstrumentRequest.Color),
                Price = InstrumentRequest.Price,
                ProductionYear = InstrumentRequest.ProductionYear,
            };

            doReMusicDbContext.Instruments.Add(NewInstrument);
            doReMusicDbContext.SaveChanges();


            return RedirectToAction("AddInstrument");
        }

        [HttpGet]
        public IActionResult DeleteInstrument([FromRoute] string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instrument = doReMusicDbContext.Instruments.Where(m => m.Id == Guid.Parse(id)).FirstOrDefault();

            if (instrument == null)
            {
                return NotFound();
            }

            doReMusicDbContext.Instruments.Remove(instrument);
            doReMusicDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateInstrument(string id)
        {
            var instrument = doReMusicDbContext.Instruments.Where(x => x.Id == Guid.Parse(id)).Include(x => x.Brand).Include(x => x.Category).FirstOrDefault(); // Implement your logic to retrieve all brands

            var viewModel = new UpdateInstrumentModel()
            {
                Id = instrument.Id.ToString(),
                Name = instrument.Name,
                Brand = instrument.Brand.Name,
                Category = instrument.Category.Name,
                Kind = instrument.Kind,
                Model = instrument.Model,
                Color = instrument.Color,
                Price = instrument.Price,
                ProductionYear = instrument.ProductionYear,
                Categories = doReMusicDbContext.Categories.ToList(),
                Brands = doReMusicDbContext.Brands.ToList(),
            };


            return View(viewModel);
        }
        [HttpPost]
        public IActionResult UpdateInstrument(UpdateInstrumentViewModel updateInstrumentModel)
        {
            Domain.Entities.Instrument originalInstrument = doReMusicDbContext.Instruments.Where(x => x.Id == Guid.Parse(updateInstrumentModel.Id)).FirstOrDefault();

            Category newCategory = doReMusicDbContext.Categories.Where(x => x.Name == updateInstrumentModel.Category).FirstOrDefault();
            Brand newBrand = doReMusicDbContext.Brands.Where(x => x.Name == updateInstrumentModel.Brand).FirstOrDefault();

            originalInstrument.Category = newCategory;
            originalInstrument.Brand = newBrand;
            originalInstrument.Kind = updateInstrumentModel.Kind;
            originalInstrument.Model = updateInstrumentModel.Model;
            originalInstrument.Name = updateInstrumentModel.Name;
            originalInstrument.Price = Convert.ToDecimal(updateInstrumentModel.Price);
            originalInstrument.ProductionYear = updateInstrumentModel.ProductionYear;
            originalInstrument.Color = (Color)Convert.ToInt32(updateInstrumentModel.Color);

            doReMusicDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult InstrumentsOfKind(string category, string kind, string sort = "default")
        {

            var instruments = doReMusicDbContext.Instruments
                .Where(x => x.Category.Name == category)
                .Where(x => x.Kind == kind)
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .ToList();

            if (sort == "alphabetic")
            {
                instruments = instruments.OrderBy(x => x.Name).ToList();
            }
            else if (sort == "ascendingbyprice")
            {
                instruments = instruments.OrderBy(x => x.Price).ToList();
            }
            else if (sort == "descendingbyprice")
            {
                instruments = instruments.OrderByDescending(x => x.Price).ToList();
            }

            if (instruments.FirstOrDefault() == null) return NotFound();
            // Pass the list of instruments to the view
            return View(instruments);
        }

        [HttpGet]
        public IActionResult SearchInstruments(string search, string sort = "default")
        {
            List<Instrument> instruments = doReMusicDbContext.Instruments.Include(x => x.Brand).Include(x => x.Category).ToList();

            List<Instrument> matchingInstruments = instruments
                .Where(instrument =>
                    instrument.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    instrument.Kind.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    instrument.Model.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    instrument.Brand.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
                )
                .ToList();

            if (sort == "alphabetic")
            {
                matchingInstruments = matchingInstruments.OrderBy(x => x.Name).ToList();
            }
            else if (sort == "ascendingbyprice")
            {
                matchingInstruments = matchingInstruments.OrderBy(x => x.Price).ToList();
            }
            else if (sort == "descendingbyprice")
            {
                matchingInstruments = matchingInstruments.OrderByDescending(x => x.Price).ToList();
            }

            SearchInstrumentViewmodel searchInstrumentViewmodel = new();
            searchInstrumentViewmodel.search = search;
            searchInstrumentViewmodel.Instruments = matchingInstruments;

            return View(searchInstrumentViewmodel);
        }

        [HttpPost]
        public IActionResult AddToCart(Guid instrumentId)
        {
            string currentUserId = Guid.NewGuid().ToString();

            HttpContext.Response.Cookies.Append("CurrentUserId", currentUserId);

            if(string.IsNullOrEmpty(currentUserId))
            {
                //
            }

            var instrument = doReMusicDbContext.Instruments.FirstOrDefault(x=>x.Id == instrumentId);

            if (instrument == null)
            {
                return NotFound();
            }

            var userCart = doReMusicDbContext.Carts.FirstOrDefault(x => x.Id == Guid.Parse(currentUserId));
            
            if(userCart == null)
            {
                userCart = new Cart { Id = Guid.Parse(currentUserId) };
                doReMusicDbContext.Carts.Add(userCart);
            }

            var cartItem = new CartItem
            {
                Instrument = instrument,
                Quantity = 1,
                Cart = userCart
            };

            return RedirectToAction("Index","Carts");
            
        }
        

    }
}
