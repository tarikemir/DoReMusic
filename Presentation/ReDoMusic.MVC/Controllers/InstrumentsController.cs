using DoReMusic.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;

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
