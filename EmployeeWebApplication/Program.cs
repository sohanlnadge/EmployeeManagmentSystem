using EmployeeWebApplication.Data;
using EmployeeWebApplication.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EmployeeWebApplication
{
    public class Program
    {
        // Main method - entry point of application
        public static async Task Main(string[] args)
        {
            // Create builder
            var builder = WebApplication.CreateBuilder(args);

            // Add MVC controllers and views
            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    // Keep JSON property names same as model
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            // Register DbContext with SQL Server
            builder.Services.AddDbContext<EmployeeDBContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("EmployeeConnection")));

            // Add Identity for authentication and roles
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<EmployeeDBContext>() // store identity data in database
                .AddDefaultTokenProviders(); // used for reset password, email confirmation etc.

            // Configure login and access denied redirects
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Events = new CookieAuthenticationEvents
                {
                    // Redirect to login page if user is not authenticated
                    OnRedirectToLogin = context =>
                    {
                        context.Response.Redirect("/Account/Login");
                        return Task.CompletedTask;
                    },

                    // Redirect if user does not have permission
                    OnRedirectToAccessDenied = contextn =>
                    {
                        contextn.Response.Redirect("/Home/UnAuthorized");
                        return Task.CompletedTask;
                    }
                };
            });

            // Build the application
            var app = builder.Build();

            // Create default roles in database
            using (var Scope = app.Services.CreateScope())
            {
                var roleManager = Scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Role names
                string[] roleNames = { "Admin", "User" };

                foreach (var roleName in roleNames)
                {
                    // Check if role exists
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        // Create role if it does not exist
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
            }

            // Configure middleware

            // Error handling for production environment
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Redirect HTTP to HTTPS
            app.UseHttpsRedirection();

            // Enable static files (CSS, JS, Images)
            app.UseStaticFiles();

            // Enable routing
            app.UseRouting();

            // Enable authorization
            app.UseAuthorization();

            // Default route configuration
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Employee}/{action=Index}/{id?}");

            // Run the application
            app.Run();
        }
    }
}