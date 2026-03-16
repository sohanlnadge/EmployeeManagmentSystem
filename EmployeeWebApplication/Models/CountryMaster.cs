using System.ComponentModel.DataAnnotations;

namespace EmployeeWebApplication.Models
{
    public class CountryMaster
    {
        [Key]
        public int CountryId { get; set; }
        [Required]
        public string CountryName { get; set; }
    }

    
}
