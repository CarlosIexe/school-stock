namespace SchoolStock.Models
{
    public class Stock : Base.BaseEntity
    {

        public long ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public long? SchoolId { get; set; }

        public School? School { get; set; }

        public int Quantity { get; set; }

        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
