using AutoMapper;
using StudentCrudApi.Data;
using StudentCrudApi.Dto;
using StudentCrudApi.Students.Model;
using StudentCrudApi.Students.Repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace StudentCrudApi.Students.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StudentRepository(AppDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<StudentDto> CreateStudent(CreateStudentRequest request)
        {
            var student = _mapper.Map<Student>(request);

            _context.Students.Add(student);

            await _context.SaveChangesAsync();

            return _mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            _context.Students.Remove(student);

            await _context.SaveChangesAsync();

            return _mapper.Map<StudentDto>(student);
        }

        public async Task<ListStudentDto> GetAllAsync()
        {
            List<Student> result = await _context.Students.ToListAsync();
            
            ListStudentDto listStudentDto = new ListStudentDto()
            {
                studentList = _mapper.Map<List<StudentDto>>(result)
            };

            return listStudentDto;
        }

        public async Task<StudentDto> GetByIdAsync(int id)
        {
            var student = await _context.Students.Where(s => s.Id == id).FirstOrDefaultAsync();
            
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto> GetByNameAsync(string name)
        {
            var student = await _context.Students.Where(s => s.Name.Equals(name)).FirstOrDefaultAsync();
            
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto> UpdateStudent(int id, UpdateStudentRequest request)
        {
            var student = await _context.Students.FindAsync(id);

            student.Name= request.Name ?? student.Name;
            student.Age=request.Age ?? student.Age;
            student.Specialization=request.Specialization ?? student.Specialization;

            _context.Students.Update(student);

            await _context.SaveChangesAsync();

            return _mapper.Map<StudentDto>(student);
        }
    }
}
