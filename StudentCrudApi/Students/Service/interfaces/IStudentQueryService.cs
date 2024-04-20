using StudentCrudApi.Students.Model;

namespace StudentCrudApi.Students.Service.interfaces
{
    public interface IStudentQueryService
    {
        Task<IEnumerable<Student>> GetAllStudents();
        Task<Student> GetByName(string name);
        Task<Student> GetById(int id);

    }
}
