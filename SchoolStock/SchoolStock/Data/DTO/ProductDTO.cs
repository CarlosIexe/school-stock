namespace SchoolStock.Data.DTO
{
    public class ProductDTO
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Code { get; set; }

        public string UnitOfMeasure { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        public int MinimumStock { get; set; }

        public long CategoryId { get; set; }

        public bool Active { get; set; } = true;
    }
}
