using StudentCrudApi.Dto;
using StudentCrudApi.Students.Model;

namespace StudentCrudApi.Students.Repository.interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByNameAsync(string name);
        Task<Student> GetByIdAsync(int id);
        Task<Student> CreateStudent(CreateStudentRequest request);
        Task<Student> UpdateStudent(int id, UpdateStudentRequest request);
        Task<Student> DeleteStudent(int id);
    }
}
