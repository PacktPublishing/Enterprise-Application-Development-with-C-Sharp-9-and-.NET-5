namespace EmployeeEF.Models
{
    public class Address
    {
        public int AddressId { get; set; }

        public int EmployeeId { get; set; }

        public string City { get; set; }

        public Employee Employee { get; set; }

    }
}