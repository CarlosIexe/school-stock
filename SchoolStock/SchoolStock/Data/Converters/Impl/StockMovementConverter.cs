using SchoolStock.Data.Converters.Contract;
using SchoolStock.Data.DTO;
using SchoolStock.Models;

namespace SchoolStock.Data.Converters.Impl
{
    public class StockMovementConverter :IObjectConverter<StockMovement, StockMovementDTO>, IObjectConverter<StockMovementDTO, StockMovement>
    {
        public StockMovementDTO Parse(StockMovement origin)
        {
            if (origin == null)
                return null!;

            return new StockMovementDTO
            {
                Id = origin.Id,
                ProductId = origin.ProductId,
                ProductName = origin.Product?.Name,
                Type = origin.Type,
                Quantity = origin.Quantity,
                OriginSchoolId = origin.OriginSchoolId,
                DestinationSchoolId = origin.DestinationSchoolId,
                CreatedAt = origin.CreatedAt,
                Observation = origin.Observation
            };
        }

        public StockMovement Parse(StockMovementDTO origin)
        {
            if (origin == null)
                return null!;

            return new StockMovement
            {
                Id = origin.Id,
                ProductId = origin.ProductId,
                Type = origin.Type,
                Quantity = origin.Quantity,
                OriginSchoolId = origin.OriginSchoolId,
                DestinationSchoolId = origin.DestinationSchoolId,
                CreatedAt = origin.CreatedAt,
                Observation = origin.Observation
            };
        }

        public List<StockMovementDTO> Parse(
            List<StockMovement> origin)
        {
            if (origin == null)
                return [];

            return origin
                .Select(Parse)
                .ToList();
        }

        public List<StockMovement> Parse(
            List<StockMovementDTO> origin)
        {
            if (origin == null)
                return [];

            return origin
                .Select(Parse)
                .ToList();
        }
    }
}
