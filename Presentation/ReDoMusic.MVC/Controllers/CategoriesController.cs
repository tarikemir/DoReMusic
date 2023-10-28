using DoReMusic.Domain.Entities;
using DoReMusic.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace DoReMusic.MVC.Controllers
{
    public class CategoriesController : Controller
    {
        public readonly DoReMusicDbContext _context;
        public CategoriesController()
        {
            _context = new DoReMusicDbContext();
        }
        [HttpGet]
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteCategory(string id)
        {
            Category category = _context.Categories.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault();
            
            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
