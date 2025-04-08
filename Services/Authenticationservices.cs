using EmployeeManagement.Data;
using EmployeeManagement.DTOs;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
public class Authenticationservices
{
    private readonly EmployeeManagementContext _context;
    private readonly IConfiguration _configuration;

    public Authenticationservices(EmployeeManagementContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<Response> Authenticate(LoginDTO info)
    {
        var userexist = await _context.Employee.FirstOrDefaultAsync(e => e.EmployeeId == info.EmployeeId);
        if (userexist == null)
        {
            return new Response
            {
                message = "Invalid EmployeeId",
                success = false
            };
        }
        if (userexist.Password != info.Password)
        {
            return new Response
            {
                message = "Invalid Password",
                success = false
            };
        }
        if (userexist.EmployeeId == info.EmployeeId && userexist.Password == info.Password)
        {
            var role = await _context.Role.FirstOrDefaultAsync(e => e.Id == userexist.RoleId);
            var token = await GenerateToken(userexist,role);
            return new Response
            {
                message = "Login Successful",
                success = true,
                Token = token
            };
        }
        return new Response
        {
            message = "Authentication failed",
            success = false
        };
    }

    public async Task<string> GenerateToken(Employee authentication,Role role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtKey = _configuration["JwtSettings:Key"];
        var jwtIssuer = _configuration["JwtSettings:Issuer"];
        var jwtAudience = _configuration["JwtSettings:Audience"];
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new Exception("JWT Key is not configured ");
        }
        var key = Encoding.ASCII.GetBytes(jwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, authentication.EmployeeId.ToString()),
                new Claim(ClaimTypes.Role, authentication.Role.RoleName),
            }),
            Issuer = jwtIssuer,
            Audience = jwtAudience,
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiryMinutes"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
