using SchoolStock.Data.DTO;

namespace SchoolStock.Services
{
    public interface IStockService
    {
        List<StockDTO> FindAll();

        StockDTO Find(
            long productId,
            long? schoolId);

        StockDTO Add(
            long productId,
            long? schoolId,
            int quantity);

        StockDTO Remove(
            long productId,
            long? schoolId,
            int quantity);

        void Transfer(
            long productId,
            long? originSchoolId,
            long? destinationSchoolId,
            int quantity);
    }
}
