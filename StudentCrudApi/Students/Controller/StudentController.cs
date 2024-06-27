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

        public override async Task<ActionResult<StudentDto>> CreateStudent([FromBody] CreateStudentRequest request)
        {
            try
            {
                var students = await _studentCommandService.CreateStudent(request);

                return Created("Studentul a fost adaugat",students);
            }
            catch (ItemAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<StudentDto>> DeleteStudent([FromRoute] int id)
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

        public override async Task<ActionResult<ListStudentDto>> GetAll()
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

        public override async Task<ActionResult<StudentDto>> GetByNameRoute([FromRoute] string name)
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

        public override async Task<ActionResult<StudentDto>> UpdateStudent([FromRoute] int id, [FromBody] UpdateStudentRequest request)
        {
            try
            {
                var students = await _studentCommandService.UpdateStudent(id,request);
                return Ok(students);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
