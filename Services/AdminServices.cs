using EmployeeManagement.Data;
using EmployeeManagement.DTOs;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

public class AdminServices
    {
        private readonly EmployeeManagementContext _context;
        public AdminServices(EmployeeManagementContext context) {

            _context = context;
        }

        public async Task<Response> RegisterEmployee(RegisterDTO info)
        {
            var userexist = await _context.Employee.FirstOrDefaultAsync(e => e.EmployeeId == info.EmployeeId);
            if (userexist != null)
            {
                return new Response { message = "Employee Id Already Exists", success = false };
            }

            var role = await _context.Role.FirstOrDefaultAsync(e => e.RoleName == info.RoleName.ToUpper());
            if (role == null)
            {
                return new Response { message = " Invalid Role , Please enter the correct role ", success = false };
            }
            var authentication = new Employee
            {
                EmployeeId = info.EmployeeId,
                Name = info.Name,
                Email = info.Email,
                Password = info.Password,
                Phone = info.Phone,
                Address = info.Address,
                RoleId = role.Id

            };

            await _context.Employee.AddAsync(authentication);
            await _context.SaveChangesAsync();
            return new Response { message = "Employee Added Successful", success = true };
        }

        public async Task<Response> AddSalary(SalaryDTO salary)
        {
        var employee = await _context.Employee.FirstOrDefaultAsync<Employee>(e => e.EmployeeId == salary.EmployeeId);
        if(employee == null)
        {
            return new Response { message = "Invalid Employee Id", success = true };
        }
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
        return new Response { message = " Salary Detials added succesfully", success = true };
        }
        public async Task<Response> DeleteEmployee(string id)
        {
        var deleteemployee = await _context.Employee.FindAsync(id);
        if(deleteemployee == null)
        {
            return new Response { message = " Employee not found", success = false };
        }
        _context.Employee.Remove(deleteemployee);
        await _context.SaveChangesAsync();
        return new Response { message = "Employee Deleted Successfully", success = true };
        }
    public async Task<IEnumerable<ViewEmployee>> ViewDetials()
    {
        return await _context.Employee.Select(e => new ViewEmployee
        {
            EmployeeId = e.EmployeeId,
            Name = e.Name,
            Email = e.Email,
            Phone = e.Phone,
            Address = e.Address,
            RoleName = e.Role.RoleName
        })
            .ToListAsync();
    }
    public async Task<IEnumerable<salary>> ViewSalary()
    {
        return await _context.salary.ToListAsync();
    }
    }

