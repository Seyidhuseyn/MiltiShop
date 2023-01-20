using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Areas.Manage.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
