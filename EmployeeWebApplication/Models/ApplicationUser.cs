using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EmployeeWebApplication.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required,StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required,StringLength(50)]
        [Display(Name = "First Name")]
        public string LastName { get; set; }

        [Required,StringLength(10)]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required,StringLength(100)]
        [Display(Name = "Address")]
        public string Address { get; set; }
    }
}
