using EmployeeManagement.DTOs;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Route ("[Controller]")]
[Authorize(policy: "Hr")]
public class HRController : ControllerBase
    {
    private readonly HRservice _service;
    public HRController(HRservice service)
    {
        _service = service;
    }

    [HttpPost("AddEmployee")]
    public async Task<ActionResult> RegisterEmployee([FromBody] RegisterDTO info)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response { success = false, message = "Invalid Input Data" });
        var result = await _service.RegisterEmployee(info);
        if (!result.success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> DeleteEmployee(string id)
    {
        var result = await _service.DeleteEmployee(id);
        if (result!.success)
            return BadRequest(result);
        return Ok(result);
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

