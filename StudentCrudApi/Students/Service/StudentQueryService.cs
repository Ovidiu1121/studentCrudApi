
using StudentCrudApi.Dto;
using StudentCrudApi.Students.Model;
using StudentCrudApi.Students.Repository.interfaces;
using StudentCrudApi.Students.Service.interfaces;
using StudentCrudApi.System.Constant;
using StudentCrudApi.System.Exceptions;

namespace StudentCrudApi.Students.Service
{
    public class StudentQueryService: IStudentQueryService
    {
        private IStudentRepository _repository;

        public StudentQueryService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ListStudentDto> GetAllStudents()
        {
            ListStudentDto students = await _repository.GetAllAsync();

            if (students.studentList.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_STUDENTS_EXIST);
            }

            return students;
        }

        public async Task<StudentDto> GetById(int id)
        {
            StudentDto students = await _repository.GetByIdAsync(id);

            if (students == null)
            {
                throw new ItemDoesNotExist(Constants.STUDENT_DOES_NOT_EXIST);
            }

            return students;
        }

        public async Task<StudentDto> GetByName(string name)
        {
            StudentDto students = await _repository.GetByNameAsync(name);

            if (students == null)
            {
                throw new ItemDoesNotExist(Constants.STUDENT_DOES_NOT_EXIST);
            }

            return students;
        }
    }
}
