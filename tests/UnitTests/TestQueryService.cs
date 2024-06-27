using System.Threading.Tasks;
using Moq;
using StudentCrudApi.Dto;
using StudentCrudApi.Students.Repository.interfaces;
using StudentCrudApi.Students.Service;
using StudentCrudApi.Students.Service.interfaces;
using StudentCrudApi.System.Constant;
using StudentCrudApi.System.Exceptions;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests;

public class TestQueryService
{
    Mock<IStudentRepository> _mock;
    IStudentQueryService _service;

    public TestQueryService()
    {
        _mock=new Mock<IStudentRepository>();
        _service=new StudentQueryService(_mock.Object);
    }
    
     [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new ListStudentDto());

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetAllStudents());

            Assert.Equal(exception.Message, Constants.NO_STUDENTS_EXIST);       

        }

        [Fact]
        public async Task GetAll_ReturnAllStudents()
        {

            var students = TestStudentFactory.CreateStudents(5);

            _mock.Setup(repo=>repo.GetAllAsync()).ReturnsAsync(students);

            var result = await _service.GetAllStudents();

            Assert.NotNull(result);
            Assert.Contains(students.studentList[1], result.studentList);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((StudentDto)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(()=>_service.GetById(1));

            Assert.Equal(Constants.STUDENT_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetById_ReturnStudent()
        {

            var student = TestStudentFactory.CreateStudent(2);

            _mock.Setup(repo => repo.GetByIdAsync(2)).ReturnsAsync(student);

            var result = await _service.GetById(2);

            Assert.NotNull(result);
            Assert.Equal(student, result);

        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.GetByNameAsync("")).ReturnsAsync((StudentDto)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByName(""));

            Assert.Equal(Constants.STUDENT_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetByName_ReturnStudent()
        {

            var student = TestStudentFactory.CreateStudent(2);
            student.Name="test";

            _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync(student);

            var result = await _service.GetByName("test");

            Assert.NotNull(result);
            Assert.Equal(student, result);

        }
    
    
    
    
}