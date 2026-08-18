using SchoolStock.Data.Converters.Contract;
using SchoolStock.Data.DTO;
using SchoolStock.Models;

namespace SchoolStock.Data.Converters.Impl
{
    public class StockConverter :
        IObjectConverter<Stock, StockDTO>,
        IObjectConverter<StockDTO, Stock>
    {
        public StockDTO Parse(Stock origin)
        {
            if (origin == null)
                return null!;

            return new StockDTO
            {
                Id = origin.Id,
                ProductId = origin.ProductId,
                ProductName = origin.Product?.Name,
                SchoolId = origin.SchoolId,
                SchoolName = origin.School?.Name,
                Quantity = origin.Quantity,
                LastUpdatedAt = origin.LastUpdatedAt
            };
        }

        public Stock Parse(StockDTO origin)
        {
            if (origin == null)
                return null!;

            return new Stock
            {
                Id = origin.Id,
                ProductId = origin.ProductId,
                SchoolId = origin.SchoolId,
                Quantity = origin.Quantity,
                LastUpdatedAt = origin.LastUpdatedAt
            };
        }

        public List<StockDTO> Parse(List<Stock> origin)
        {
            if (origin == null)
                return [];

            return origin
                .Select(Parse)
                .ToList();
        }

        public List<Stock> Parse(List<StockDTO> origin)
        {
            if (origin == null)
                return [];

            return origin
                .Select(Parse)
                .ToList();
        }
    }
}
