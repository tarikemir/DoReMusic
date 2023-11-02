using DoReMusic.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace DoReMusic.MVC.Controllers
{
    public class CartsController : Controller
    {
        public readonly DoReMusicDbContext _context;

        public CartsController()
        {
            _context = new DoReMusicDbContext();
        }
        public IActionResult Index(string id)
        {
            var user = _context.Users.SingleOrDefault(x=> x.Id == Guid.Parse(id));
            var cart = user.Cart;

            return View(cart);
        }
    }
}
