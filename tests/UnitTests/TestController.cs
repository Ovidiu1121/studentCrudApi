using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentCrudApi.Dto;
using StudentCrudApi.Students.Controller;
using StudentCrudApi.Students.Controller.interfaces;
using StudentCrudApi.Students.Service.interfaces;
using StudentCrudApi.System.Constant;
using StudentCrudApi.System.Exceptions;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests;

public class TestController
{
    
    Mock<IStudentCommandService> _command;
    Mock<IStudentQueryService> _query;
    StudentApiController _controller;

    public TestController()
    {
        _command = new Mock<IStudentCommandService>();
        _query = new Mock<IStudentQueryService>();
        _controller = new StudentController(_command.Object, _query.Object);
    }
    
       [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {

            _query.Setup(repo => repo.GetAllStudents()).ThrowsAsync(new ItemDoesNotExist(Constants.STUDENT_DOES_NOT_EXIST));
           
            var result = await _controller.GetAll();

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(404, notFound.StatusCode);
            Assert.Equal(Constants.STUDENT_DOES_NOT_EXIST, notFound.Value);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {

            var students = TestStudentFactory.CreateStudents(5);

            _query.Setup(repo => repo.GetAllStudents()).ReturnsAsync(students);

            var result = await _controller.GetAll();
            var okresult = Assert.IsType<OkObjectResult>(result.Result);
            var studentsAll = Assert.IsType<ListStudentDto>(okresult.Value);

            Assert.Equal(5, studentsAll.studentList.Count);
            Assert.Equal(200, okresult.StatusCode);


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

                _command.Setup(repo => repo.CreateStudent(It.IsAny<CreateStudentRequest>())).ThrowsAsync(new ItemAlreadyExists(Constants.STUDENT_ALREADY_EXIST));

                var result = await _controller.CreateStudent(create);
                var bad=Assert.IsType<BadRequestObjectResult>(result.Result);

                Assert.Equal(400,bad.StatusCode);
                Assert.Equal(Constants.STUDENT_ALREADY_EXIST, bad.Value);

            }

            [Fact]
            public async Task Create_ValidData()
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

                _command.Setup(repo => repo.CreateStudent(create)).ReturnsAsync(student);

                var result = await _controller.CreateStudent(create);

                var okResult= Assert.IsType<CreatedResult>(result.Result);

                Assert.Equal(okResult.StatusCode, 201);
                Assert.Equal(student, okResult.Value);

            }

            [Fact]
            public async Task Update_InvalidDate()
            {

                var update = new UpdateStudentRequest()
                {
                    Name="Test",
                    Age=0,
                    Specialization= "test"
                };

                _command.Setup(repo => repo.UpdateStudent(11, update)).ThrowsAsync(new ItemDoesNotExist(Constants.STUDENT_DOES_NOT_EXIST));

                var result = await _controller.UpdateStudent(11, update);

                var bad = Assert.IsType<NotFoundObjectResult>(result.Result);

                Assert.Equal(bad.StatusCode, 404);
                Assert.Equal(bad.Value, Constants.STUDENT_DOES_NOT_EXIST);

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

            _command.Setup(repo=>repo.UpdateStudent(5,update)).ReturnsAsync(student);

            var result = await _controller.UpdateStudent(5, update);

            var okResult=Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, student);

        }


        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {

            _command.Setup(repo=>repo.DeleteStudent(2)).ThrowsAsync(new ItemDoesNotExist(Constants.STUDENT_DOES_NOT_EXIST));

            var result= await _controller.DeleteStudent(2);

            var notfound= Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notfound.StatusCode, 404);
            Assert.Equal(notfound.Value, Constants.STUDENT_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var student = TestStudentFactory.CreateStudent(1);

            _command.Setup(repo => repo.DeleteStudent(1)).ReturnsAsync(student);

            var result = await _controller.DeleteStudent(1);

            var okResult=Assert.IsType<AcceptedResult>(result.Result);

            Assert.Equal(202, okResult.StatusCode);
            Assert.Equal(student, okResult.Value);

        }
    
}