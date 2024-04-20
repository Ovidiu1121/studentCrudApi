
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

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            IEnumerable<Student> students = await _repository.GetAllAsync();

            if (students.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_STUDENTS_EXIST);
            }

            return students;
        }

        public async Task<Student> GetById(int id)
        {
            Student students = await _repository.GetByIdAsync(id);

            if (students == null)
            {
                throw new ItemDoesNotExist(Constants.STUDENT_DOES_NOT_EXIST);
            }

            return students;
        }

        public async Task<Student> GetByName(string name)
        {
            Student students = await _repository.GetByNameAsync(name);

            if (students == null)
            {
                throw new ItemDoesNotExist(Constants.STUDENT_DOES_NOT_EXIST);
            }

            return students;
        }
    }
}
