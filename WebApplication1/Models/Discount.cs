using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Discount:BaseEntity
    {
        public string Name { get; set; }
        [Range(0.0, Double.MaxValue)]
        public double Percenttage { get; set; }
        public ICollection<Product>?  Products { get; set; }
    }
}
