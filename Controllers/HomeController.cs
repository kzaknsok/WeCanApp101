using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp2._1._7.Data;
using MyApp2._1._7.Models;
using MyApp2._1._7.Services;
using System.Diagnostics;

namespace MyApp2._1._7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserService _userService;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserService userService, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userService = userService;//不要かも
            _userManager = userManager;//不要かも
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Word.Include(w => w.PostUser).Include(w => w.UpdateUser);
            return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
