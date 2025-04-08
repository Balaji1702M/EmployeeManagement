using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class salary
    {
        [Key]
        public int SalaryId { get; set; }
        public string EmployeeId { get; set; }
        public decimal Salary { get; set; }
        public decimal PF { get; set; }
        public decimal Esi { get; set; }
        public DateOnly SalaryUpdatedDate { get; set; }


    }
}