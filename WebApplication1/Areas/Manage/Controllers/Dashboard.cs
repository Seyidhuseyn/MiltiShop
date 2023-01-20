using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class Dashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
