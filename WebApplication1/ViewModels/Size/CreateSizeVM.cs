using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class CreateSizeVM
    {
        public string Name { get; set; }
        public ICollection<ProductSize>? ProductSizes { get; set; }
    }
}
