using EmployeeWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWebApplication.Data
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    

    }
}
