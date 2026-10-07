namespace SchoolStock.Data.DTO.Stock
{
    public class StockRemoveRequest
    {
        public long ProductId { get; set; }
        public long? SchoolId { get; set; }
        public int Quantity { get; set; }
    }
}