using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.DTOs;
using Microsoft.EntityFrameworkCore;

public class TLServices
{
    private readonly EmployeeManagementContext _context;
    public TLServices(EmployeeManagementContext context)
    {
        _context = context;
    }
    public async Task<Response> AddSalary(SalaryDTO salary)
    {
        var Employee = await _context.Employee.FirstOrDefaultAsync(e => e.EmployeeId == salary.EmployeeId);
        if (Employee == null)
            return new Response { message = "Invalid EmployeeID", success = false };
        if (Employee.RoleId != "DEVOLEPER101")
            return new Response { message = "Sorry you cant add salary to this Person", success = false };
        var Salary = new salary
        {
            EmployeeId = salary.EmployeeId,
            Salary = salary.Salary,
            PF = salary.PF,
            Esi = salary.Esi,
            SalaryUpdatedDate = salary.SalaryUpdatedDate
        };
        await _context.salary.AddAsync(Salary);
        await _context.SaveChangesAsync();
        return new Response { message = " Salary Added Succesfully", success = true };
    }
    public async Task<IEnumerable<ViewEmployee>> ViewEmployeeDetials()
    {
        var employee = await _context.Employee.Where(e => e.RoleId == "DEVOLEPER101")
            .Include(e => e.Role)
            .ToListAsync();
        if (employee == null)
        {
            return null;
        }
        return employee.Select(employee => new ViewEmployee
        {
            EmployeeId = employee.EmployeeId,
            Name = employee.Name,
            Email = employee.Email,
            Phone = employee.Phone,
            Address = employee.Address,
            RoleName = employee.Role.RoleName
        }).ToList();
    }
    public async Task<IEnumerable<salary>> ViewSalaries()
    {
      return await _context.Employee.Where(e => e.RoleId == "DEVOLEPER101")
     .SelectMany(e => e.Salary)
     .ToListAsync();
    }
}
