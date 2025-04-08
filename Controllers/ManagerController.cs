using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using EmployeeManagement.DTOs;
using Microsoft.AspNetCore.Authorization;
[Authorize(policy: "Manager")]
[ApiController]
[Route("[Controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly ManagerService _service;
        public ManagerController(ManagerService service)
        {
            _service = service;
        }
        [HttpGet("ViewEmployee")]
        public async Task <ActionResult<IEnumerable<Employee>>> Employeedetial()
        {
            var employee = await _service.ViewEmployees();
            if (employee == null)
                return BadRequest();
            return Ok(employee);
        }
        [HttpPost("AddSalary")]
        public async Task<IActionResult> AddSalarydetials([FromBody] SalaryDTO salary)
        {
            if (!ModelState.IsValid)
                return BadRequest(new Response { success = false, message = " Invalid Input Data " });
            var result = await _service.Addsalary(salary);
            if (!result.success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("ViewSalaries")]
        public async Task<ActionResult<IEnumerable<salary>>> ViewAllSalary()
        {
            var salary = await _service.ViewEmployeeSalaries();
            return Ok(salary);
        }
    }
