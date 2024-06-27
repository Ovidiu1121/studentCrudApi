using Microsoft.AspNetCore.Mvc;
using StudentCrudApi.Dto;
using StudentCrudApi.Students.Model;

namespace StudentCrudApi.Students.Controller.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class StudentApiController:ControllerBase
    {
        [HttpGet("all")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Student>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<ListStudentDto>> GetAll();

        [HttpPost("create")]
        [ProducesResponseType(statusCode: 201, type: typeof(Student))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<StudentDto>> CreateStudent([FromBody] CreateStudentRequest request);

        [HttpPut("update/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Student))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<StudentDto>> UpdateStudent([FromRoute] int id, [FromBody] UpdateStudentRequest request);

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Student))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<StudentDto>> DeleteStudent([FromRoute] int id);

        [HttpGet("name/{name}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Student))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<StudentDto>> GetByNameRoute([FromRoute] string name);
    }
}
