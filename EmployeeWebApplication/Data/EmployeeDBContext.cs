using EmployeeWebApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWebApplication.Data
{
    public class EmployeeDBContext : IdentityDbContext<ApplicationUser>
    {
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<CountryMaster> CountryMasters { get; set; }



    }
}
