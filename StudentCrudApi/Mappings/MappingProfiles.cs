using StudentCrudApi.Dto;
using StudentCrudApi.Students.Model;
using AutoMapper;

namespace StudentCrudApi.Mappings
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateStudentRequest, Student>();
            CreateMap<UpdateStudentRequest, Student>();
        }
    }
}
