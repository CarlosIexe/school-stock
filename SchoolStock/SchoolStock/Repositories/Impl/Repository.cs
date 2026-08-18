using Microsoft.EntityFrameworkCore;
using SchoolStock.Models.Base;
using SchoolStock.Models.Context;

namespace SchoolStock.Repositories.Impl
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private AppDbContext _context;
        private DbSet<T> _dataset;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dataset = context.Set<T>();
        }

        public T Create(T entity)
        {
                _context.Add(entity);
                _context.SaveChanges();
                return entity;
        }

        public void Delete(long id)
        {
            var existingItem = _dataset.Find(id);
            if (existingItem == null) return;
            _context.Remove(existingItem);

            _context.SaveChanges();
        }

        public List<T> FindAll()
        {
            return _dataset.ToList();
        }

        public T FindById(long id)
        {
            return _dataset.Find(id);
        }

        public T Update(T entity)
        {
            var existingItem = _dataset.Find(entity.Id);
            if (existingItem == null) { return null; }

            _context.Entry(existingItem).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
