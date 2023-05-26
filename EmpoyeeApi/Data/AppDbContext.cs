using EmpoyeeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmpoyeeApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
