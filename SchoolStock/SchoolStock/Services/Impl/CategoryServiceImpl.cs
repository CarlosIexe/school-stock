using SchoolStock.Data.Converters.Impl;
using SchoolStock.Data.DTO;
using SchoolStock.Models;
using SchoolStock.Repositories;

namespace SchoolStock.Services.Impl
{
    public class CategoryServiceImpl : ICategoryService
    {
        private readonly IRepository<Category> _repository;
        private readonly CategoryConverter _converter;

        public CategoryServiceImpl(IRepository<Category> repository)
        {
            _repository = repository;
            _converter = new CategoryConverter();
        }

        public CategoryDTO Create(CategoryDTO category)
        {
            var entity = _converter.Parse(category);

            entity = _repository.Create(entity);

            return _converter.Parse(entity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<CategoryDTO> FindAll()
        {
            return _converter.Parse(
                _repository.FindAll());
        }

        public CategoryDTO FindById(long id)
        {
            return _converter.Parse(
                _repository.FindById(id));
        }

        public CategoryDTO Update(CategoryDTO category)
        {
            var entity = _converter.Parse(category);

            entity = _repository.Update(entity);

            return _converter.Parse(entity);
        }
    }
}
