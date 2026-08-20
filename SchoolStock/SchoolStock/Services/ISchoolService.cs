using SchoolStock.Data.DTO;

namespace SchoolStock.Services
{
    public interface ISchoolService
    {
        SchoolDTO Create(SchoolDTO school);
        SchoolDTO FindById(long id);
        List<SchoolDTO> FindAll();
        SchoolDTO Update(SchoolDTO school);
        void Delete(long id);
    }
}
