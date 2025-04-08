using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public string Password { get; set; }
    }
}