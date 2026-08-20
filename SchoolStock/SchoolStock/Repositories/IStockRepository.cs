using SchoolStock.Models;

namespace SchoolStock.Repositories
{
    public interface IStockRepository
    {
        Stock? Find(long productId, long? schoolId);
        List<Stock> FindBySchool(long schoolId);
        List<Stock> FindAll();
        public Stock Update(Stock stock);
        public Stock Create(Stock stock);
        void Save(Stock stock);

    }
}
