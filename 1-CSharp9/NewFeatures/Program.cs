using System;

namespace NewFeatures
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeRecord employee = new EmployeeRecord("Suneel", "Kunani");
            EmployeeRecord employee2 = new EmployeeRecord("Suneel", "Kunani");
            

            Console.WriteLine(employee == employee2);
        }
    }

}
