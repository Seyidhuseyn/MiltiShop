namespace WebApplication1.Models
{
    public class Slider:BaseEntity
    {
        public string PrimaryTitle { get; set; }
        public string SecondaryTitle { get; set; }
        public string ImageUrl { get; set; }
        public int Order { get; set; }
    }
}
