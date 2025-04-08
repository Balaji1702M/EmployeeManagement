
using System.ComponentModel.DataAnnotations;


namespace EmployeeManagement.Models
{
    public class Role
    {
        [Key]
        public string Id { get; set; }
        [Required]
        
        public string RoleName { get; set; }

        public ICollection<Employee> Employee { get; set; }
    }
}