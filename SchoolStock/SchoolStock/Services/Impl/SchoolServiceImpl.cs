using SchoolStock.Data.Converters.Impl;
using SchoolStock.Data.DTO;
using SchoolStock.Models;
using SchoolStock.Repositories;

namespace SchoolStock.Services.Impl
{
    public class SchoolServiceImpl : ISchoolService
    {
        private readonly IRepository<School> _repository;
        private readonly SchoolConverter _converter;

        public SchoolServiceImpl(IRepository<School> repository)
        {
            _repository = repository;
            _converter = new SchoolConverter();
        }

        public SchoolDTO Create(SchoolDTO school)
        {
            var entity = _converter.Parse(school);

            entity = _repository.Create(entity);

            return _converter.Parse(entity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<SchoolDTO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public SchoolDTO FindById(long id)
        {
            return _converter.Parse( _repository.FindById(id));
        }

        public SchoolDTO Update(SchoolDTO school)
        {
            var entity = _converter.Parse(school);

            entity = _repository.Update(entity);

            return _converter.Parse(entity);
        }
    }
}
