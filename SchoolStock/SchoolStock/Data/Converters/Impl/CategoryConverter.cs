using SchoolStock.Data.Converters.Contract;
using SchoolStock.Data.DTO;
using SchoolStock.Models;

namespace SchoolStock.Data.Converters.Impl
{
    public class CategoryConverter : IObjectConverter<Category, CategoryDTO>, IObjectConverter<CategoryDTO, Category>
    {
        public CategoryDTO Parse(Category origin)
        {
            if (origin == null)
                return null!;

            return new CategoryDTO
            {
                Id = origin.Id,
                Name = origin.Name,
                Active = origin.Active
            };
        }

        public Category Parse(CategoryDTO origin)
        {
            if (origin == null)
                return null!;

            return new Category
            {
                Id = origin.Id,
                Name = origin.Name,
                Active = origin.Active
            };
        }

        public List<CategoryDTO> Parse(List<Category> origin)
        {
            if (origin == null)
                return [];

            return origin
                .Select(Parse)
                .ToList();
        }

        public List<Category> Parse(List<CategoryDTO> origin)
        {
            if (origin == null)
                return [];

            return origin
                .Select(Parse)
                .ToList();
        }

    }
}
