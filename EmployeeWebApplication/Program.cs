using EmployeeWebApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container 
            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    //A property naming policy or null to leave property names unchange
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            // Register the DbContext with SQL server using the connection string from appsetting.jeson
            builder.Services.AddDbContext<EmployeeDBContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("EmployeeConnection")));

            var app = builder.Build();

            // Configure middleware
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Employee}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
