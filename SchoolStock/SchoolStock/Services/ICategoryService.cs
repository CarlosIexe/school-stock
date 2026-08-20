using SchoolStock.Data.DTO;

namespace SchoolStock.Services
{
    public interface ICategoryService
    {
        CategoryDTO Create(CategoryDTO category);
        CategoryDTO FindById(long id);
        List<CategoryDTO> FindAll();
        CategoryDTO Update(CategoryDTO category);
        void Delete(long id);
    }
}
