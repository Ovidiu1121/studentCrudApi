namespace StudentCrudApi.Dto;

public class ListStudentDto
{
    public ListStudentDto()
    {
        studentList = new List<StudentDto>();
    }
    
    public List<StudentDto> studentList { get; set; }
}