using SchoolStock.Data.DTO;

namespace SchoolStock.Services
{
    public interface IStockMovementService
    {
        List<StockMovementDTO> FindAll();

        StockMovementDTO FindById(long id);
    }
}
