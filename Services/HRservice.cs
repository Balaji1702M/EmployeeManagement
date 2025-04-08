using EmployeeManagement.Data;
using EmployeeManagement.DTOs;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Models;

    public class HRservice
    {
        private readonly EmployeeManagementContext _context;
        public HRservice(EmployeeManagementContext context)
        {
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
        public async Task<Response> DeleteEmployee(string id)
        {
            var deleteemployee = await _context.Employee.FindAsync(id);
            if (deleteemployee == null)
            {
                return new Response { message = " Employee not found", success = false };
            }
            _context.Employee.Remove(deleteemployee);
            await _context.SaveChangesAsync();
            return new Response { message = "Employee Deleted Successfully", success = true };
        }
        public async Task<Response> Addsalary(SalaryDTO salary)
        {
            var employee = await _context.Employee.FirstOrDefaultAsync(e => e.EmployeeId == salary.EmployeeId);
            if (employee == null)
            {
                return new Response { message = " Invalid Employee Id", success = false };
            }
            if (employee.RoleId == "ADMIN101")
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
            return new Response { message = " Salary Updated succesfully", success = false };
        }
        public async Task<IEnumerable<salary>> ViewEmployeeSalaries()
        {
            var salary = await _context.salary.ToListAsync();
            if (salary == null)
                return null;
            return salary;
        }
    }

