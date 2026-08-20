using Microsoft.EntityFrameworkCore;
using SchoolStock.Models;
using SchoolStock.Models.Context;

namespace SchoolStock.Repositories.Impl
{
    public class StockRepositoryImpl : IStockRepository
    {
        private readonly AppDbContext _context;
        public StockRepositoryImpl(AppDbContext context)
        {
            _context = context;
        }
        public Stock? Find(long productId, long? schoolId)
        {
            return _context.Stocks.FirstOrDefault(s => s.ProductId == productId && s.SchoolId == schoolId);
        }

        public List<Stock> FindAll()
        {
           return _context.Stocks.Include(s=>s.Product).Include(s=>s.School).AsNoTracking().ToList();
        }

        public List<Stock> FindBySchool(long schoolId)
        {
            return _context.Stocks.Include(s => s.Product).Where(s => s.SchoolId == schoolId).AsNoTracking().ToList();
        }
        public Stock Create(Stock stock)
        {
            _context.Stocks.Add(stock);
            _context.SaveChanges();

            return stock;
        }

        public Stock Update(Stock stock)
        {
            _context.Stocks.Update(stock);
            _context.SaveChanges();

            return stock;
        }

        public void Save(Stock stock)
        {
            if(stock.Id == 0) _context.Stocks.Add(stock);
            _context.SaveChanges();
        }
    }
}
