namespace SchoolStock.Data.DTO.Stock
{
    public class StockTransferRequest
    {
        public long ProductId { get; set; }
        public long? OriginSchoolId { get; set; }
        public long? DestinationSchoolId { get; set; }
        public int Quantity { get; set; }
    }
}