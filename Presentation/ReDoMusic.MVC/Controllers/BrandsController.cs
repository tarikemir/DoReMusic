using Microsoft.AspNetCore.Mvc;
using DoReMusic.Domain.Entities;
using DoReMusic.Persistence.Contexts;
using DoReMusic.MVC.Models;
using DoReMusic.MVC.ViewModels;

namespace ReDoMusic.MVC.Controllers
{
    public class BrandsController : Controller
    {
        private readonly DoReMusicDbContext _context;
        public BrandsController()
        {
            _context = new();
        }
        public IActionResult Index()
        {
            var brands = _context.Brands.ToList();
            return View(brands);
        }
        [HttpGet]
        public IActionResult AddBrand()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBrand(string brandName, string brandDisplayText, string brandAddress)
        {
            var brand = new Brand()
            {
                Id = Guid.NewGuid(),
                Name = brandName,
                Address = brandAddress,
                DisplayText = brandDisplayText,
                CreatedOn = DateTime.UtcNow
            };

            _context.Brands.Add(brand);
            _context.SaveChanges();
            return View();
        }

        [HttpGet]
        public IActionResult DeleteBrand(string id)
        {
            var brand = _context.Brands.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault();
            _context.Brands.Remove(brand);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateBrand(string id)
        {
            var brand = _context.Brands.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault(); // Implement your logic to retrieve all brands

            var viewModel = new UpdateViewModel()
            {
                Id = id,
                Name = brand.Name,
                Address = brand.Address,
                DisplayText = brand.DisplayText,
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult UpdateBrand(UpdateBrandRequest updateBrandRequest)
        {
            var originalBrand = _context.Brands.Where(x => x.Id == Guid.Parse(updateBrandRequest.BrandId)).FirstOrDefault();

            originalBrand.Name = updateBrandRequest.Name;
            originalBrand.DisplayText = updateBrandRequest.DisplayText;
            originalBrand.Address = updateBrandRequest.Address;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}
