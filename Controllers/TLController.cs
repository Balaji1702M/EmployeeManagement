using EmployeeManagement.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

    [Route("[Controller]")]
    [ApiController]
    [Authorize(policy:"TeamLeader")]
    public class TLController : ControllerBase
    {
        private readonly TLServices _services;
        public TLController(TLServices services)
        {
            _services = services;
        }
        [HttpPost("AddSalary")]
        public async Task<ActionResult> Addsalary([FromBody] SalaryDTO salary)
        {
            if (!ModelState.IsValid)
                return BadRequest(new Response { message = "Invalid Input Data", success = false });
            var addsalary = await _services.AddSalary(salary);
            if (!addsalary.success)
                return BadRequest(addsalary);
            return Ok(addsalary);
        }
        [HttpGet("ViewDetials")]
        public async Task<ActionResult> ViewEmployeeDetials()
        {
            var result =  await _services.ViewEmployeeDetials();
            return Ok(result);
        }
        [HttpGet("ViewSalaries")]
        public async Task<ActionResult> ViewSalaries()
        {
            var result = await _services.ViewSalaries();
            return Ok(result);
        }
    }

