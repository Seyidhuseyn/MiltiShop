namespace WebApplication1.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public string ImgUrl { get; set; }
        public ICollection<Product>? Products { get; set;}
    }
}
