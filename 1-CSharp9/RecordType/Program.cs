using System;

namespace RecordType
{
    class Program
    {
        static void Main(string[] args)
        {
            Shape s1 = new Shape("Shape");
            Shape s2 = new Shape("Shape");
            // ToString of record is overwritten to print the properties of the type
            Console.WriteLine(s1.ToString());
            // GetHashCode of record is overwritten to generate teh hash code based on values
            Console.WriteLine($"HashCode of s1 is : {s1.GetHashCode()}");
            Console.WriteLine($"HashCode of s2 is : {s2.GetHashCode()}");
            // Equality operatio of record type is overwriten to check equality based on the values
            Console.WriteLine($"Is s1 equals s2 : {s1 == s2}");


            // Create instance of derived class with the copy of base instace
            Person person = new("Suneel", "Kunani");
            Person person2 = new("Mahanya", "Kunani");
        }
    }

    public record Person(string FirstName, string LastName);

    public record Shape(string Name);
    public record Circle : Shape
    {
        public double Radius { get; }
        public Circle(string Name, double radius)
            : base(Name) => this.Radius = radius;
    }
}
