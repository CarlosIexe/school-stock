namespace SchoolStock.Models
{
    public class Category : Base.BaseEntity
    {

        public string Name { get; set; } = string.Empty;

        public bool Active { get; set; } = true;

        public ICollection<Product> Products { get; set; } = [];
    }
}
