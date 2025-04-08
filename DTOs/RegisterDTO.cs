    using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.DTOs
{
    public class RegisterDTO
    {
        public string EmployeeId { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public string RoleName { get; set; }
    }
}
