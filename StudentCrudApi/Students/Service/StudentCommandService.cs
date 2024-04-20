using StudentCrudApi.Dto;
using StudentCrudApi.Students.Model;
using StudentCrudApi.Students.Repository.interfaces;
using StudentCrudApi.Students.Service.interfaces;
using StudentCrudApi.System.Constant;
using StudentCrudApi.System.Exceptions;

namespace StudentCrudApi.Students.Service
{
    public class StudentCommandService: IStudentCommandService
    {
        private IStudentRepository _repository;

        public StudentCommandService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Student> CreateStudent(CreateStudentRequest request)
        {
            Student student = await _repository.GetByNameAsync(request.Name);

            if (student!=null)
            {
                throw new ItemAlreadyExists(Constants.STUDENT_ALREADY_EXIST);
            }

            student=await _repository.CreateStudent(request);
            return student;
        }

        public async Task<Student> DeleteStudent(int id)
        {
            Student student = await _repository.GetByIdAsync(id);

            if (student==null)
            {
                throw new ItemDoesNotExist(Constants.STUDENT_DOES_NOT_EXIST);
            }

            await _repository.DeleteStudent(id);
            return student;
        }

        public async Task<Student> UpdateStudent(int id, UpdateStudentRequest request)
        {

            Student product = await _repository.GetByIdAsync(id);

            if (product==null)
            {
                throw new ItemDoesNotExist(Constants.STUDENT_DOES_NOT_EXIST);
            }

            product = await _repository.UpdateStudent(id, request);
            return product;
        }
    }
}
