namespace WebApplication1.Models.Base
{
    public class BaseAccountableEntity
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string Createdby { get; set; }
    }
}
