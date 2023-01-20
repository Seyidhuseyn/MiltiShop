using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        [Range(0.0, Double.MaxValue)]
        public double SellPrice { get; set; }
        [Range(0.0, Double.MaxValue)]
        public double CostPrice { get; set; }
        public string Description { get; set; }
        public int? DiscountId { get; set; }
        public Discount? Discount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ProductInformationId { get; set; }

        public ICollection<ProductSize>? ProductSizes { get; set;}
        public ICollection<ProductColor>? ProductColors { get; set;}
        public ICollection<ProductImage>? ProductImages { get; set;}
        public ProductInformation? ProductInformation { get; set; }
    }
}
