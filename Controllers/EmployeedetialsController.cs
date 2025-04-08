using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagement.Controllers
{
    [Route("api/[Controller]")]
    [Authorize]
    public class EmployeedetialsController : ControllerBase
    {
        private readonly Employeedetialsservices _Employeedetialsservices;
        public EmployeedetialsController(Employeedetialsservices employee ) 
        {
            _Employeedetialsservices = employee;
        }

        [HttpGet("Detials")]
        public async Task<ActionResult> EmployeeDetials()
        {
            var employee = _Employeedetialsservices.GetEmployeedetial(User);
            if (employee == null)
                return NotFound(new Response { message = " Employee not found ", success = false });
            return Ok(employee);
        }
        [HttpGet("SalaryDetials")]

        public async Task<ActionResult> SalaryDetials()
        {
            var salary = await _Employeedetialsservices.GetSalaryDetials(User);
            if (salary == null)
                return NotFound(new Response { message = "Salary not found", success = false });
            return Ok(salary);
        }

    }
}
