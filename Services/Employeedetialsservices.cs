using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

    public class Employeedetialsservices
    {
        private readonly EmployeeManagementContext _context;
        public Employeedetialsservices(EmployeeManagementContext context)
        {
            _context = context;
        }
        public async Task<List<salary>> GetSalaryDetials(ClaimsPrincipal Employee)
        {
            var employeeId = Employee.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(employeeId))
                return null;
            return await _context.salary.Where(e => e.EmployeeId == employeeId).ToListAsync();
        }
        public ViewEmployee GetEmployeedetial(ClaimsPrincipal Employee)
        {
        var employeeId = Employee.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(employeeId))
            return null;
        var employee = _context.Employee.FirstOrDefault(e => e.EmployeeId == employeeId);
        var role = _context.Role.FirstOrDefault(e => e.Id == employee.RoleId);
        return new ViewEmployee
        {
            EmployeeId = employee.EmployeeId,
            Name = employee.Name,
            Email = employee.Email,
            RoleName = role.RoleName,
            Address = employee.Address,
            Phone = employee.Phone
        };

        }

    }
