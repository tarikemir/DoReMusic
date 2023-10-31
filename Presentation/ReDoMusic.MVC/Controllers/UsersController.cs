using DoReMusic.Domain.Entities;
using DoReMusic.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ExceptionServices;

namespace DoReMusic.MVC.Controllers
{
    public class UsersController : Controller
    {
        public readonly DoReMusicDbContext _context;

        public UsersController()
        {
            _context = new DoReMusicDbContext();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _context.Users.ToList();

            return View(users);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(string firstName, string lastName, string email, string password)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                CreatedOn = DateTime.UtcNow

            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return View();
        }
    }
}
