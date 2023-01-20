using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.ViewModels.Home;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Sliders = _context.Sliders,
                Products= _context.Products,
                Categories= _context.Categories
            };
            return View(homeVM);
        }
    }
}
