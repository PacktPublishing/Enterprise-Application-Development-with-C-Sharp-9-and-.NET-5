using System.Linq;
using EmployeeEF.Models;

namespace EmployeeEF
{
    public static class DbContextExtension 
    {
        public static void SeedData(this EmployeeContext context)
        {
            SeedEmployees(context);            
        }

        private static void SeedEmployees(EmployeeContext context)
        {
            if (context.Employees.Any())
            {
                return;
            }
            var employees = new Employee[]
            {
                new Employee{EmployeeId = 1, Name = "Sample1", Email="Sample@sample.com"},
                new Employee{EmployeeId = 2, Name = "Sample2", Email="Sample2@sample.com"},
                new Employee{EmployeeId = 3, Name = "Sample3", Email="Sample3@sample.com"},
            };
            context.Employees.AddRange(employees);
            var adresses = new Address[]
            {
                new Address{AddressId = 1, City = "City1", EmployeeId = 1},
                new Address{AddressId = 2, City = "City2", EmployeeId = 1},
                new Address{AddressId = 3, City = "City1", EmployeeId = 2},
            };
            context.Addresses.AddRange(adresses);
            context.SaveChanges();
        }
    }
}