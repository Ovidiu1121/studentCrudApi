using StudentCrudApi.Dto;
using StudentCrudApi.Students.Model;

namespace StudentCrudApi.Students.Service.interfaces
{
    public interface IStudentCommandService
    {
        Task<StudentDto> CreateStudent(CreateStudentRequest request);
        Task<StudentDto> UpdateStudent(int id, UpdateStudentRequest request);
        Task<StudentDto> DeleteStudent(int id);
    }
}
