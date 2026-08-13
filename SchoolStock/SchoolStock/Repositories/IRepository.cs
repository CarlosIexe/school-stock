using SchoolStock.Models.Base;

namespace SchoolStock.Repositories

{
    public interface IRepository<T> where T : BaseEntity
    {
        List<T> FindAll();

        T FindById(long id);

        T Create(T entity);

        T Update(T entity);

        void Delete(long id);
    }
}
