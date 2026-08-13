using SchoolStock.Models.Enums;

namespace SchoolStock.Models
{
    public class StockMovement
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public MovementType Type { get; set; }

        public int Quantity { get; set; }

        public long? OriginSchoolId { get; set; }

        public long? DestinationSchoolId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? Observation { get; set; }
    }
}
