using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.Utilies.Extension;
using WebApplication1.ViewModels;

namespace WebApplication1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env = null)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Sliders.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateSliderVM sliderVM)
        {
            IFormFile file = sliderVM.Image;
            string result = file?.CheckValidate("image/", 300);
            if (result?.Length>0)
            {
                ModelState.AddModelError("ImageUrl", result);
            }
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            using (var stream = new FileStream(Path.Combine(_env.WebRootPath, "img", "slider", fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Slider newSlider = new Slider
            {
                PrimaryTitle= sliderVM.PrimaryTitle,
                SecondaryTitle= sliderVM.SecondaryTitle,
                Order=sliderVM.Order,
                ImageUrl = fileName
            };
            if (_context.Sliders.Any(s=>s.Order==newSlider.Order))
            {
                ModelState.AddModelError("Order", $"{newSlider.Order} sirasinda artiq slider movcuddur.");
                return View();
            }
            _context.Sliders.Add(newSlider);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id==0) return BadRequest();
            Slider slider = _context.Sliders.Find(id);
            _context.Sliders.Remove(slider);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}