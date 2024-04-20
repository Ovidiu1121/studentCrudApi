using Microsoft.AspNetCore.Mvc;
using StudentCrudApi.Dto;
using StudentCrudApi.Students.Controller.interfaces;
using StudentCrudApi.Students.Model;
using StudentCrudApi.Students.Repository.interfaces;
using StudentCrudApi.Students.Service.interfaces;
using StudentCrudApi.System.Exceptions;

namespace StudentCrudApi.Students.Controller
{
    public class StudentController:StudentApiController
    {
        private IStudentCommandService _studentCommandService;
        private IStudentQueryService _studentQueryService;

        public StudentController(IStudentCommandService studentCommandService, IStudentQueryService studentQueryService)
        {
            _studentCommandService = studentCommandService;
            _studentQueryService = studentQueryService;
        }

        public override async Task<ActionResult<Student>> CreateStudent([FromBody] CreateStudentRequest request)
        {
            try
            {
                var students = await _studentCommandService.CreateStudent(request);

                return Ok(students);
            }
            catch (ItemAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<Student>> DeleteStudent([FromRoute] int id)
        {

            try
            {
                var students = await _studentCommandService.DeleteStudent(id);

                return Accepted("", students);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<Student>>> GetAll()
        {
            try
            {
                var students = await _studentQueryService.GetAllStudents();
                return Ok(students);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<Student>> GetByNameRoute([FromRoute] string name)
        {
            try
            {
                var students = await _studentQueryService.GetByName(name);
                return Ok(students);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<Student>> UpdateStudent([FromRoute] int id, [FromBody] UpdateStudentRequest request)
        {
            try
            {
                var students = await _studentQueryService.GetById(id);
                return Ok(students);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
