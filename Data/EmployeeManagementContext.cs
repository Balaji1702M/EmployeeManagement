using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Models;
namespace EmployeeManagement.Data
{
public partial class EmployeeManagementContext : DbContext
{
    public EmployeeManagementContext(DbContextOptions<EmployeeManagementContext> options)
        : base(options)
    {
    }   
    public DbSet<Role> Role { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<salary> salary { get; set; }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
    {
            modelbuilder.Entity<Employee>()
            .HasKey(e => e.EmployeeId);

            modelbuilder.Entity<Employee>()
                .HasOne(e => e.Role)
                .WithMany(r => r.Employee)
                .HasForeignKey(e => e.RoleId);

            modelbuilder.Entity<salary>()
            .HasKey(s => s.SalaryId);

            modelbuilder.Entity<Role>()
            .HasKey(r => r.Id);

            modelbuilder.Entity<salary>()
            .Property(s => s.SalaryId)
            .ValueGeneratedOnAdd();

            modelbuilder.Entity<salary>()
                .Property(s => s.Salary)
                .HasColumnType("decimal(18,2)");

            modelbuilder.Entity<salary>()
                .Property(s => s.PF)
                .HasColumnType("decimal(18,2)");

            modelbuilder.Entity<salary>()
                .Property(s => s.Esi)
                .HasColumnType("decimal(18,2)");
        }
}
}