using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class CreateProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Range(0.0, Double.MaxValue)]
        public double SellPrice { get; set; }
        [Range(0.0, Double.MaxValue)]
        public double CostPrice { get; set; }
        public string Description { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; }
        public IFormFile CoverImage { get; set; }
        public ICollection<IFormFile>? OtherImages { get; set; }
        public List<int> ColorIds { get; set; }
        public List<int> SizeIds { get; set; }
        public int DiscountIds { get; set; }
        public Discount? Discount { get; set; }
        public int ProductInformationId { get; set; }
        public ProductInformation? ProductInformation { get; set; }
    }
}
