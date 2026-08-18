using SchoolStock.Data.Converters.Contract;
using SchoolStock.Data.DTO;
using SchoolStock.Models;

namespace SchoolStock.Data.Converters.Impl
{
    public class SchoolConverter :IObjectConverter<School, SchoolDTO>, IObjectConverter<SchoolDTO, School>
    {
        public SchoolDTO Parse(School origin)
        {
            if (origin == null)
                return null!;

            return new SchoolDTO
            {
                Id = origin.Id,
                Name = origin.Name,
                InepCode = origin.InepCode,
                Address = origin.Address,
                Phone = origin.Phone,
                PrincipalName = origin.PrincipalName,
                StudentCount = origin.StudentCount,
                Active = origin.Active
            };
        }

        public School Parse(SchoolDTO origin)
        {
            if (origin == null)
                return null!;

            return new School
            {
                Id = origin.Id,
                Name = origin.Name,
                InepCode = origin.InepCode,
                Address = origin.Address,
                Phone = origin.Phone,
                PrincipalName = origin.PrincipalName,
                StudentCount = origin.StudentCount,
                Active = origin.Active
            };
        }

        public List<SchoolDTO> Parse(List<School> origin)
        {
            if (origin == null)
                return [];

            return origin
                .Select(Parse)
                .ToList();
        }

        public List<School> Parse(List<SchoolDTO> origin)
        {
            if (origin == null)
                return [];

            return origin
                .Select(Parse)
                .ToList();
        }
    }
}
