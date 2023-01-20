using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class CreateCategoryVM
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public string ImgUrl { get; set; }
        public IFormFile Image { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}