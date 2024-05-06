using Microsoft.EntityFrameworkCore;
using RStudents.Models;

namespace RStudents.Services
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Student> Students { get; set; }

    }

}
