using Microsoft.AspNetCore.Mvc;
using DoReMusic.Domain.Entities;
using DoReMusic.Persistence.Contexts;

namespace ReDoMusic.MVC.Controllers
{
    public class BrandController : Controller
    {
        private readonly DoReMusicDbContext _context;
        public BrandController()
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
        public IActionResult AddBrand( string brandName, string brandDisplayText, string brandAddress)
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
    }
}
