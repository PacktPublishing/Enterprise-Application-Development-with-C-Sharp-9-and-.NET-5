using System;
using System.Runtime.CompilerServices;

namespace StaticAnonymousFunction
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        string formatString = $"{0}-{1}";

        void GenerateSummary(string[] args)
        {
            GenerateOrderReport(static () =>
            {
                // return formatString; // Will get error
                return $"Order Id:{1}, Order Date:{1}";
            });
        }

        static string GenerateOrderReport(Func<string> getFormatString)
        {
            var order = new
            {
                Orderid = 1,
                OrderDate = DateTime.Now
            };
            return string.Format(getFormatString(), order.Orderid);
        }
    }
}
