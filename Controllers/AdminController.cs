using EmployeeManagement.DTOs;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Route("admin")]
[ApiController]
[Authorize(policy: "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AdminServices _services;
        public AdminController(AdminServices adminServices)
        {
            _services = adminServices;
        }

        [HttpPost("AddEmployee")]
        public async Task<ActionResult> RegisterEmployee([FromBody] RegisterDTO info)
        {
            if (!ModelState.IsValid)
                return BadRequest(new Response { success = false, message = "Invalid Input Data" });
            var result = await _services.RegisterEmployee(info);
            if (!result.success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("Delete{id}")]
        public async Task<ActionResult> DeleteEmployee(string id)
        {
            var result = await _services.DeleteEmployee(id);
            if (result!.success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("AddSalary")]
        public async Task<ActionResult> AddSalarydetials([FromBody] SalaryDTO salary)
        {
            if (!ModelState.IsValid)
                return BadRequest(new Response { success = false, message = " Invalid Input Data " });
            var result = await _services.AddSalary(salary);
            if(!result.success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("ViewEmployees")]
        public async Task<ActionResult<IEnumerable<Employee>>> ViewEmployeedetials()
        {
            var employees = await _services.ViewDetials();
            return Ok(employees);
        }

        [HttpGet("ViewSalaries")]
        public async Task<ActionResult<IEnumerable<salary>>> ViewAllSalary()
        {   
            var salary = await _services.ViewSalary();
            return Ok(salary);
        }
    }
