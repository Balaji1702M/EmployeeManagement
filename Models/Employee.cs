using System.ComponentModel.DataAnnotations;
namespace EmployeeManagement.Models
{
    public class Employee
    {
        [Key]
        public string EmployeeId { get; set; }
        [Required]
        public string Password { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<salary> Salary { get; set; }  
    }
}