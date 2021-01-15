using System.Collections.Generic;

namespace EmployeeEF.Models
{
     public class Employee
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public ICollection<Address> Address { get; set; }

    }
}