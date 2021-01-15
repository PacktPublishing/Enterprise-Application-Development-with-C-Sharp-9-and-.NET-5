using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthSample.Core
{
    public class MinimumAgeAuthorizationHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        MinimumAgeRequirement requirement)
        {
            if (context.User.HasClaim(
                c => c.Type == ClaimTypes.DateOfBirth))
            {
                var dateOfBirth = Convert.ToDateTime(
                    context.User.FindFirst(x =>
                    x.Type == ClaimTypes.DateOfBirth).Value);

                var age = DateTime.Today.Year - dateOfBirth.Year;

                if (dateOfBirth > DateTime.Today.AddYears(-age)) age--;

                if (age >= requirement.MinimumAge)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }

}
