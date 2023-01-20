using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ColorController : Controller
    {
        readonly AppDbContext _context;

        public ColorController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Colors.ToList());
        }
        public IActionResult Create()
        {
            ViewBag.Colors = new SelectList(_context.Colors, nameof(Color.Id), nameof(Color.Name));
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateColorVM colorVM)
        {
            if (colorVM==null) return NotFound();
            Color color = new Color { Name= colorVM.Name};
            _context.Colors.Add(color);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id==null) return BadRequest();
            Color color = _context.Colors.Find(id);
            _context.Colors.Remove(color);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
