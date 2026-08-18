using SchoolStock.Data.DTO;
using SchoolStock.Repositories;
using SchoolStock.Models;
using SchoolStock.Data.Converters.Impl;

namespace SchoolStock.Services.Impl
{
    public class ProductServiceImpl : IProductService
    {
        private IRepository<Product> _repository;
        private readonly ProductConverter _converter;

        public ProductServiceImpl (IRepository<Product> repository)
        {
            _repository = repository;
            _converter = new ProductConverter();
        }

        public ProductDTO Create(ProductDTO product)
        {
            var entity = _converter.Parse(product);
            entity = _repository.Create(entity);
            return _converter.Parse(entity);
        }

        public void Delete(long id)
        {
           _repository.Delete(id);
        }

        public List<ProductDTO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public ProductDTO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public ProductDTO Update(ProductDTO product)
        {
            var entity = _converter.Parse(product);
            entity = _repository.Update(entity);
            return _converter.Parse(entity);
        }
    }
}
