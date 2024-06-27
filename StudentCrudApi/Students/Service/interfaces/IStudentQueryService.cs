using StudentCrudApi.Dto;
using StudentCrudApi.Students.Model;

namespace StudentCrudApi.Students.Service.interfaces
{
    public interface IStudentQueryService
    {
        Task<ListStudentDto> GetAllStudents();
        Task<StudentDto> GetByName(string name);
        Task<StudentDto> GetById(int id);

    }
}
