using AutoMapper;
using StudentCrudApi.Data;
using StudentCrudApi.Dto;
using StudentCrudApi.Students.Model;
using StudentCrudApi.Students.Repository.interfaces;
using System.Data.Entity;

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

        public async Task<Student> CreateStudent(CreateStudentRequest request)
        {
            var student = _mapper.Map<Student>(request);

            _context.Students.Add(student);

            await _context.SaveChangesAsync();

            return student;
        }

        public async Task<Student> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            _context.Students.Remove(student);

            await _context.SaveChangesAsync();

            return student;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await this._context.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
           return await _context.Students.FirstOrDefaultAsync(obj => obj.Id.Equals(id));
        }

        public async Task<Student> GetByNameAsync(string name)
        {
            return await _context.Students.FirstOrDefaultAsync(obj => obj.Name.Equals(name));
        }

        public async Task<Student> UpdateStudent(int id, UpdateStudentRequest request)
        {
            var student = await _context.Students.FindAsync(id);

            student.Name= request.Name ?? student.Name;
            student.Age=request.Age ?? student.Age;
            student.Specialization=request.Specialization ?? student.Specialization;

            _context.Students.Update(student);

            await _context.SaveChangesAsync();

            return student;
        }
    }
}
