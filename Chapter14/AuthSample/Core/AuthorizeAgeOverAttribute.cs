using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSample.Core
{
    public class AuthorizeAgeOverAttribute : AuthorizeAttribute
    {
        const string POLICY_PREFIX = "Over";
        public AuthorizeAgeOverAttribute(int age) => Age = age;

        public int Age
        {
            get
            {
                if (int.TryParse(Policy.Substring(POLICY_PREFIX.Length), out var age))
                {
                    return age;
                }
                return default(int);
            }
            set
            {
                Policy = $"{POLICY_PREFIX}{value.ToString()}";
            }
        }
    }

}
