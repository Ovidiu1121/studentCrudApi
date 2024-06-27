using StudentCrudApi.Dto;

namespace tests.Helpers;

public class TestStudentFactory
{
    public static StudentDto CreateStudent(int id)
    {
        return new StudentDto
        {
            Id = id,
            Name="Alex"+id,
            Age=43+id,
            Specialization=""+id
        };
    }

    public static ListStudentDto CreateStudents(int count)
    {
        ListStudentDto students=new ListStudentDto();
            
        for(int i = 0; i<count; i++)
        {
            students.studentList.Add(CreateStudent(i));
        }
        return students;
    }
}