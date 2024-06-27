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

public class TestCommandService
{
    Mock<IStudentRepository> _mock;
    IStudentCommandService _service;

    public TestCommandService()
    {
        _mock = new Mock<IStudentRepository>();
        _service = new StudentCommandService(_mock.Object);
    }
    
        [Fact]
        public async Task Create_InvalidData()
        {
            var create = new CreateStudentRequest()
            {
                Name="Test",
                Age=0,
                Specialization= "test"
            };

            var student = TestStudentFactory.CreateStudent(5);

            _mock.Setup(repo => repo.GetByNameAsync("Test")).ReturnsAsync(student);
                
           var exception=  await Assert.ThrowsAsync<ItemAlreadyExists>(()=>_service.CreateStudent(create));

            Assert.Equal(Constants.STUDENT_ALREADY_EXIST, exception.Message);



        }

        [Fact]
        public async Task Create_ReturnStudent()
        {

            var create = new CreateStudentRequest()
            {
                Name="Test",
                Age=0,
                Specialization= "test"
            };

            var student = TestStudentFactory.CreateStudent(5);
            student.Name=create.Name;
            student.Age=create.Age;
            student.Specialization=create.Specialization;

            _mock.Setup(repo => repo.CreateStudent(It.IsAny<CreateStudentRequest>())).ReturnsAsync(student);

            var result = await _service.CreateStudent(create);

            Assert.NotNull(result);
            Assert.Equal(result, student);
        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var update = new UpdateStudentRequest()
            {
                Name="Test",
                Age=0,
                Specialization= "test"
            };

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((StudentDto)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateStudent(1, update));

            Assert.Equal(Constants.STUDENT_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task Update_InvalidData()
        {
            var update = new UpdateStudentRequest()
            {
                Name="Test",
                Age=0,
                Specialization= "test"
            };

            _mock.Setup(repo=>repo.GetByIdAsync(1)).ReturnsAsync((StudentDto)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateStudent(5, update));

            Assert.Equal(Constants.STUDENT_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task Update_ValidData()
        {
            var update = new UpdateStudentRequest()
            {
                Name="Test",
                Age=0,
                Specialization= "test"
            };

            var student = TestStudentFactory.CreateStudent(5);
            student.Name=update.Name;
            student.Age=update.Age.Value;
            student.Specialization=update.Specialization;

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(student);
            _mock.Setup(repoo => repoo.UpdateStudent(It.IsAny<int>(), It.IsAny<UpdateStudentRequest>())).ReturnsAsync(student);

            var result = await _service.UpdateStudent(5, update);

            Assert.NotNull(result);
            Assert.Equal(student, result);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.DeleteStudent(It.IsAny<int>())).ReturnsAsync((StudentDto)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeleteStudent(5));

            Assert.Equal(exception.Message, Constants.STUDENT_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var student = TestStudentFactory.CreateStudent(5);

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(student);

            var result= await _service.DeleteStudent(1);

            Assert.NotNull(result);
            Assert.Equal(student, result);


        }
    
    
}