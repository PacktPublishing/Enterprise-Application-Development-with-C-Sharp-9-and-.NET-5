using EmployeeEF.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeEF
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set;}
        public DbSet<Address> Addresses { get; set;}

         public EmployeeContext (DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Address>().ToTable("Address");
        }

    }
}