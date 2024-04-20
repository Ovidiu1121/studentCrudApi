using Microsoft.EntityFrameworkCore;
using StudentCrudApi.Students.Model;

namespace StudentCrudApi.Data
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Student> Students { get; set; }

    }
}
