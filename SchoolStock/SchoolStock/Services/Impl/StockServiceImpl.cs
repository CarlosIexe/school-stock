using SchoolStock.Data.Converters.Impl;
using SchoolStock.Data.DTO;
using SchoolStock.Models;
using SchoolStock.Models.Context;
using SchoolStock.Models.Enums;
using SchoolStock.Repositories;

namespace SchoolStock.Services.Impl
{
    public class StockServiceImpl : IStockService
    {
        private readonly IStockRepository _stockRepository;

        private readonly IRepository<StockMovement> _movementRepository;

        private readonly StockConverter _stockConverter;

        private readonly StockMovementConverter _movementConverter;

        private readonly AppDbContext _context;


        public StockServiceImpl(
            IStockRepository stockRepository,
            IRepository<StockMovement> movementRepository,
            AppDbContext context)
        {
            _stockRepository = stockRepository;

            _movementRepository = movementRepository;

            _context = context;

            _stockConverter = new StockConverter();

            _movementConverter = new StockMovementConverter();
        }


        public List<StockDTO> FindAll()
        {
            var stocks = _stockRepository.FindAll();

            return _stockConverter.Parse(stocks);
        }


        public StockDTO Find(long productId, long? schoolId)
        {
            var stock = _stockRepository.Find(productId, schoolId);

            if (stock == null)
                throw new Exception("Estoque não encontrado.");

            return _stockConverter.Parse(stock);
        }


        public StockDTO Add(long productId, long? schoolId, int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("A quantidade deve ser maior que zero.");

            var stock = _stockRepository.Find(productId, schoolId);


            if (stock == null)
            {
                stock = new Stock
                {
                    ProductId = productId,
                    SchoolId = schoolId,
                    Quantity = quantity,
                    LastUpdatedAt = DateTime.UtcNow
                };

                stock = _stockRepository.Create(stock);
            }
            else
            {
                stock.Quantity += quantity;

                stock.LastUpdatedAt = DateTime.UtcNow;

                stock = _stockRepository.Update(stock);
            }


            RegisterMovement(
                productId,
                MovementType.Entry,
                quantity,
                null,
                schoolId,
                "Entrada de estoque"
            );


            return _stockConverter.Parse(stock);
        }


        public StockDTO Remove( long productId, long? schoolId, int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("A quantidade deve ser maior que zero.");

            var stock = _stockRepository.Find(productId, schoolId);


            if (stock == null) throw new Exception("Estoque não encontrado.");


            if (stock.Quantity < quantity) throw new Exception("Quantidade insuficiente em estoque.");


            stock.Quantity -= quantity;

            stock.LastUpdatedAt = DateTime.UtcNow;


            stock = _stockRepository.Update(stock);


            RegisterMovement(
                productId,
                MovementType.Consumption,
                quantity,
                schoolId,
                null,
                "Saída de estoque"
            );


            return _stockConverter.Parse(stock);
        }


        public void Transfer(long productId, long? originSchoolId, long? destinationSchoolId, int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("A quantidade deve ser maior que zero.");


            if (originSchoolId == destinationSchoolId) throw new ArgumentException( "A origem e o destino não podem ser iguais.");


            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var originStock =_stockRepository.Find(productId, originSchoolId);


                if (originStock == null) throw new Exception("Estoque de origem não encontrado.");


                if (originStock.Quantity < quantity) throw new Exception("Quantidade insuficiente no estoque de origem.");


                originStock.Quantity -= quantity;

                originStock.LastUpdatedAt = DateTime.UtcNow;


                _stockRepository.Update(originStock);



                var destinationStock = _stockRepository.Find(productId, destinationSchoolId);


                if (destinationStock == null)
                {
                    destinationStock = new Stock
                    {
                        ProductId = productId,
                        SchoolId = destinationSchoolId,
                        Quantity = quantity,
                        LastUpdatedAt = DateTime.UtcNow
                    };

                    _stockRepository.Create(destinationStock);
                }
                else
                {
                    destinationStock.Quantity += quantity;

                    destinationStock.LastUpdatedAt = DateTime.UtcNow;

                    _stockRepository.Update(destinationStock);
                }


                RegisterMovement(
                    productId,
                    MovementType.Transfer,
                    quantity,
                    originSchoolId,
                    destinationSchoolId,
                    "Transferência de estoque"
                );


                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();

                throw;
            }
        }


        private void RegisterMovement(
            long productId,
            MovementType type,
            int quantity,
            long? originSchoolId,
            long? destinationSchoolId,
            string? observation)
        {
            var movement =
                new StockMovement
                {
                    ProductId = productId,

                    Type = type,

                    Quantity = quantity,

                    OriginSchoolId =
                        originSchoolId,

                    DestinationSchoolId =
                        destinationSchoolId,

                    CreatedAt =
                        DateTime.UtcNow,

                    Observation =
                        observation
                };


            _movementRepository.Create(movement);
        }

    }
}
