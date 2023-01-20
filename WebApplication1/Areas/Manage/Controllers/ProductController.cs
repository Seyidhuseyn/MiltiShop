using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.Utilies.Extension;
using WebApplication1.ViewModels;

namespace WebApplication1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env = null)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Products.Include(p=>p.ProductColors).ThenInclude(pc=>pc.Color).Include(p=>p.ProductSizes).ThenInclude(ps=>ps.Size)
                .Include(p=>p.ProductImages).ThenInclude(pc=>pc.ImageUrl));
        }
        public IActionResult Create()
        {
            ViewBag.Colors = new SelectList(_context.Colors, nameof(Color.Id), nameof(Color.Name));
            ViewBag.Sizes = new SelectList(_context.Sizes, nameof(Size.Id), nameof(Size.Name));
            ViewBag.Discounts = new SelectList(_context.Discounts, nameof(Discount.Id), nameof(Discount.Name));
            ViewBag.Informations = new SelectList(_context.ProductInformations, nameof(ProductInformation.Id), nameof(ProductInformation.Name));
            return View();
        }        
        [HttpPost]
        public IActionResult Create(CreateProductVM cp)
        {
            var coverImg = cp.CoverImage;
            var otherImgs = cp.OtherImages ?? new List<IFormFile>();
            string result = coverImg.CheckValidate("image/", 300);
            if (result?.Length>0)
            {
                ModelState.AddModelError("CoverImage", result);
            }
            foreach (IFormFile image in otherImgs)
            {
                result = image.CheckValidate("image/", 300);
                if (result?.Length>0)
                {
                    ModelState.AddModelError("OtherImage", result);
                }
            }
            foreach (int colorId in (cp.ColorIds ?? new List<int>()))
            {
                if (!_context.Colors.Any(c=>c.Id==colorId))
                {
                    ModelState.AddModelError("ColorIds", "yeniden daxil edin.");
                    break;
                }
            }
            foreach (int sizeId in cp.SizeIds)
            {
                if (!_context.Sizes.Any(s=>s.Id==sizeId))
                {
                    ModelState.AddModelError("SizeIds", "Yeniden daxil edin.");
                    break;
                }
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Colors = new SelectList(_context.Colors, nameof(Color.Id), nameof(Color.Name));
                ViewBag.Sizes = new SelectList(_context.Sizes, nameof(Size.Id), nameof(Size.Name));
                ViewBag.DiscountSs = new SelectList(_context.Discounts, nameof(Discount.Id), nameof(Discount.Name));
                ViewBag.Informations = new SelectList(_context.ProductInformations, nameof(ProductInformation.Id), nameof(ProductInformation.Name));
                return View();
            }
            var sizes = _context.Sizes.Where(s => cp.SizeIds.Contains(s.Id));
            var colors = _context.Colors.Where(c=>cp.ColorIds.Contains(c.Id));
            Product newproduct = new Product
            {
                Name = cp.Name,
                SellPrice = cp.SellPrice,
                CostPrice = cp.CostPrice,
                Description = cp.Description,
                Discount = cp.Discount
            };
            List<ProductImage> images = new List<ProductImage>();
            images.Add(new ProductImage { ImageUrl = coverImg.SaveFile(Path.Combine(_env.WebRootPath, "img", "Product")), Product=newproduct });
            foreach (var item in otherImgs)
            {
                images.Add(new ProductImage { ImageUrl=item.SaveFile(Path.Combine(_env.WebRootPath, "img", "Product")), Product = newproduct });
            }
            newproduct.ProductImages= images;
            _context.Products.Add(newproduct);
            foreach (var item in sizes)
            {
                _context.ProductSizes.Add(new ProductSize { Product = newproduct, SizeId = item.Id });
            }
            foreach (var item in colors)
            {
                _context.ProductColors.Add(new ProductColor { Product = newproduct, ColorId = item.Id });
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}
