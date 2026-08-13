namespace SchoolStock.Models
{
    public class Category
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool Active { get; set; } = true;

        public ICollection<Product> Products { get; set; } = [];
    }
}
