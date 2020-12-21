using System;
using System.Runtime.CompilerServices;

namespace ClassLibrary1
{
    
    public class Class1
    {
        private class ABCD
        {
            [ModuleInitializer]
            protected static void Initialize()
            {
                Console.WriteLine("Module Initialization1");
            }

        }
    }
}
