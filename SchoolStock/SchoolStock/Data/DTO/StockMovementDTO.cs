using SchoolStock.Models.Enums;

namespace SchoolStock.Data.DTO
{
    public class StockMovementDTO
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public string? ProductName { get; set; }

        public MovementType Type { get; set; }

        public int Quantity { get; set; }

        public long? OriginSchoolId { get; set; }

        public long? DestinationSchoolId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? Observation { get; set; }
    }
}
