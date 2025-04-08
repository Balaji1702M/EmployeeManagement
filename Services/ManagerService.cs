using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagement.DTOs;
using Microsoft.EntityFrameworkCore;

[Authorize(policy: "Manager")]
    public class ManagerService
    {
        private readonly EmployeeManagementContext _context;
        public ManagerService(EmployeeManagementContext Context)
        {
            _context = Context;
        }
    public async Task<IEnumerable<ViewEmployee>> ViewEmployees()
    {
        var employees = await _context.Employee.Where(e => e.RoleId!= "ADMIN101" && e.RoleId!="HR101")
            .Include(e => e.Role) 
            .ToListAsync();

        if (employees == null)
            return null;

        return employees.Select(employee => new ViewEmployee
        {
            EmployeeId = employee.EmployeeId,
            Name = employee.Name,
            Email = employee.Email,
            Phone = employee.Phone,
            Address = employee.Address,
            RoleName = employee.Role.RoleName
        }).ToList();
    }
    public async Task<Response> Addsalary(SalaryDTO salary)
    {
        var employee = await _context.Employee.FirstOrDefaultAsync(e => e.EmployeeId == salary.EmployeeId);
        if (employee == null)
        {
            return new Response { message = " Invalid Employee Id", success = false };
        }
        if (employee.RoleId == "ADMIN101" || employee.RoleId == "HR101")
            return new Response { message = " Sorry You cant add salary for this Person", success = false };
        var addsalary = new salary
        {
            EmployeeId = salary.EmployeeId,
            Salary = salary.Salary,
            PF = salary.PF,
            Esi = salary.Esi,
            SalaryUpdatedDate = salary.SalaryUpdatedDate
        };
        await _context.salary.AddAsync(addsalary);
        await _context.SaveChangesAsync();
        return new Response { message = " Salary Added succesfully", success = false };
    }
    public async Task<IEnumerable<salary>> ViewEmployeeSalaries()
    {
        var salary = await _context.salary.ToListAsync();
        if (salary == null)
            return null;
        return salary;
    }
}
    

