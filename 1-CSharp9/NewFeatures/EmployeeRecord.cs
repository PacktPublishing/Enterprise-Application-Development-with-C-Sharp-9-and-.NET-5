using System;
using System.Collections.Generic;
using System.Text;

namespace NewFeatures
{
    public record EmployeeRecord
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public EmployeeRecord(string first, string last) => (FirstName, LastName) = (first, last);
    }

    public record MyClass(string Test);
    public class MyClass1
    {
        public int MyProperty { get; init; }
    }

    class MyClass4
    {
        public int MyProperty { get; }
        public int MyProperty2 { get; set; }
        public MyClass4(int p) => MyProperty = p;

        public void x()

        {
            MyClass4 myClass4 = new MyClass4(1)
            {
                MyProperty2 = 1
            };
        }
    }

}
