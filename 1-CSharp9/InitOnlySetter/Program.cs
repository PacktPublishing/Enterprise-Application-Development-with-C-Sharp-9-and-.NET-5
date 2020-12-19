using System;
using System.Data;

namespace InitOnlySetter
{
    public class Order
    {
        public int OrderId { get; init; }
        public decimal TotalPrice { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Order orderObject = new Order
            {
                OrderId = 1,
                TotalPrice = 10.0M
            };

            // We will get an error if we try to change the OrderId post object creation
            // orderObject.OrderId = 100;

            Console.WriteLine($"Order: Id: {orderObject.OrderId}, Total price: {orderObject.TotalPrice}");
        }
    }
}
