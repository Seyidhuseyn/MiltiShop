using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.Utilies.Extension;
using WebApplication1.ViewModels;

namespace WebApplication1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;

        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_context.Categories.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateCategoryVM categoryVM)
        {
            IFormFile file = categoryVM.Image;
            string result = file?.CheckValidate("image/", 300);
            if (result?.Length>0)
            {
                ModelState.AddModelError("ImageUrl", result);
            }
            string fileName = Guid.NewGuid().ToString()+file.FileName;
            using (var stream = new FileStream(Path.Combine(_env.WebRootPath, "img", "category", fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Category newCategory = new Category
            {
                Name = categoryVM.Name,
                Count = categoryVM.Count,
                ImgUrl = fileName
            };
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id==null || id==0) return BadRequest();
            Category category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update()
        {
            return View();
        }
    }
}
