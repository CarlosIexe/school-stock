using SchoolStock.Data.DTO;

namespace SchoolStock.Services
{
    public interface IProductService
    {
        List<ProductDTO> FindAll();

        ProductDTO FindById(long id);

        ProductDTO Create(ProductDTO dto);

        ProductDTO Update(ProductDTO dto);

        void Delete(long id);
    
    }
}
