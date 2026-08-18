namespace SchoolStock.Data.DTO
{
    public class StockDTO
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public string? ProductName { get; set; }

        public long? SchoolId { get; set; }

        public string? SchoolName { get; set; }

        public int Quantity { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
