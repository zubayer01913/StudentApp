using Microsoft.EntityFrameworkCore;
using StudentApp.Models;

namespace StudentApp
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<StudentInfo> StudentInfos { get; set; }
    }
}
