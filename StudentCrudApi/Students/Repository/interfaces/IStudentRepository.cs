using StudentCrudApi.Dto;
using StudentCrudApi.Students.Model;

namespace StudentCrudApi.Students.Repository.interfaces
{
    public interface IStudentRepository
    {
        Task<ListStudentDto> GetAllAsync();
        Task<StudentDto> GetByNameAsync(string name);
        Task<StudentDto> GetByIdAsync(int id);
        Task<StudentDto> CreateStudent(CreateStudentRequest request);
        Task<StudentDto> UpdateStudent(int id, UpdateStudentRequest request);
        Task<StudentDto> DeleteStudent(int id);
    }
}
