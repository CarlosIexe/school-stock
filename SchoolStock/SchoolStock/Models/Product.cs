namespace SchoolStock.Models
{
    public class Product : Base.BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Code { get; set; }

        public string UnitOfMeasure { get; set; } = "UN";

        public decimal UnitPrice { get; set; }

        public int MinimumStock { get; set; }

        public bool Active { get; set; } = true;

        public long CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public ICollection<Stock> Stocks { get; set; } = [];

        public ICollection<StockMovement> Movements { get; set; } = [];
    }
}
