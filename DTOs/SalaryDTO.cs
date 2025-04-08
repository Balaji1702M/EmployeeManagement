namespace EmployeeManagement.DTOs
{
    public class SalaryDTO
    {
        public string EmployeeId { get; set; }
        public decimal Salary { get; set; }
        public decimal PF { get; set; }
        public decimal Esi { get; set; }
        public DateOnly SalaryUpdatedDate { get; set; }

    }
}
