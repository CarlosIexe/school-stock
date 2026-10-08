using SchoolStock.Data.Converters.Impl;
using SchoolStock.Data.DTO;
using SchoolStock.Models;
using SchoolStock.Repositories;
using SchoolStock.Exceptions;

namespace SchoolStock.Services.Impl
{
    public class StockMovementServiceImpl : IStockMovementService
    {
        private readonly IRepository<StockMovement> _repository;

        private readonly StockMovementConverter _converter;


        public StockMovementServiceImpl(IRepository<StockMovement> repository)
        {
            _repository = repository;

            _converter = new StockMovementConverter();
        }


        public List<StockMovementDTO> FindAll()
        {
            var movements = _repository.FindAll();

            return _converter.Parse(movements);
        }


        public StockMovementDTO FindById(long id)
        {
            var movement = _repository.FindById(id);

            if (movement == null) throw new NotFoundException("Movimentação não encontrada.");

            return _converter.Parse(movement);
        }
    }
}
