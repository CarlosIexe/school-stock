using SchoolStock.Data.Converters.Contract;
using SchoolStock.Data.DTO;
using SchoolStock.Models;

namespace SchoolStock.Data.Converters.Impl
{
    public class ProductConverter : IObjectConverter<Product, ProductDTO>,
    IObjectConverter<ProductDTO, Product>
    {
        public ProductDTO Parse(Product origin)
        {
            if (origin == null)
                return null!;

            return new ProductDTO
            {
                Id = origin.Id,
                Name = origin.Name,
                Description = origin.Description,
                Code = origin.Code,
                UnitOfMeasure = origin.UnitOfMeasure,
                UnitPrice = origin.UnitPrice,
                MinimumStock = origin.MinimumStock,
                CategoryId = origin.CategoryId,
                Active = origin.Active
            };
        }

        public Product Parse(ProductDTO origin)
        {
            if (origin == null)
                return null!;

            return new Product
            {
                Id = origin.Id,
                Name = origin.Name,
                Description = origin.Description,
                Code = origin.Code,
                UnitOfMeasure = origin.UnitOfMeasure,
                UnitPrice = origin.UnitPrice,
                MinimumStock = origin.MinimumStock,
                CategoryId = origin.CategoryId,
                Active = origin.Active
            };
        }

        public List<Product> Parse(List<ProductDTO> origin)
        {
            if (origin == null)
                return [];

            return origin
                .Select(Parse)
                .ToList();
        }

        public List<ProductDTO> Parse(List<Product> origin)
        {
            if (origin == null)
                return [];

            return origin
                .Select(Parse)
                .ToList();
        }
    }
}
