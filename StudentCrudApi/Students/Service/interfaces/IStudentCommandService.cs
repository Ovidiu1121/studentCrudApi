using StudentCrudApi.Dto;
using StudentCrudApi.Students.Model;

namespace StudentCrudApi.Students.Service.interfaces
{
    public interface IStudentCommandService
    {
        Task<Student> CreateStudent(CreateStudentRequest request);
        Task<Student> UpdateStudent(int id, UpdateStudentRequest request);
        Task<Student> DeleteStudent(int id);
    }
}
