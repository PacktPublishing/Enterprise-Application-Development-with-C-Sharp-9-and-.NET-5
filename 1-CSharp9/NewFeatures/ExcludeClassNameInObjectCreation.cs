using System;
using System.Collections.Generic;
using System.Text;

namespace NewFeatures
{
    public class ExcludeClassNameInObjectCreation
    {
        public TestClass CreateObject()
        {
            TestClass testClass = new();
            return testClass;
        }
    }
}
