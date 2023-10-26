using DoReMusic.Domain.Entities;
using DoReMusic.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DoReMusic.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly DoReMusicDbContext doReMusicDbContext;

        public HomeController()
        {
            doReMusicDbContext = new DoReMusicDbContext();
        }

        [HttpGet]
        public IActionResult Index()
        {

            List<Category> categories = doReMusicDbContext.Categories.ToList();
            return View(categories);
        }
    }
}
