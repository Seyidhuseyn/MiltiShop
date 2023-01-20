using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class CreateColorVM
    {
        public string Name { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; }
    }
}
