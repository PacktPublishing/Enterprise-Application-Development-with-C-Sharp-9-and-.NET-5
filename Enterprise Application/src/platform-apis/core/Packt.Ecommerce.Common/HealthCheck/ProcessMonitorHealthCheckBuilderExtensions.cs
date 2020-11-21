// "//-----------------------------------------------------------------------".
// <copyright file="ProcessMonitorHealthCheckBuilderExtensions.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Microsoft.Extensions.DependencyInjection
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Packt.Ecommerce.Common.HealthCheck;

    /// <summary>
    /// Extenstion class to register ProcessMonitor HealthCheck.
    /// </summary>
    public static class ProcessMonitorHealthCheckBuilderExtensions
    {
        /// <summary>
        /// Regster the Process monitor Health check.
        /// </summary>
        /// <param name="builder">Health check builder.</param>
        /// <param name="processName">Name of the process to monitor.</param>
        /// <param name="name">Name of the health check.</param>
        /// <param name="failureStatus">Failure status.</param>
        /// <param name="tags">A list of tags that can be used for filtering health checks.</param>
        /// <returns>Health check builder for chaining.</returns>
        public static IHealthChecksBuilder AddProcessMonitorHealthCheck(
            this IHealthChecksBuilder builder,
            string processName = default,
            string name = default,
            HealthStatus? failureStatus = default,
            IEnumerable<string> tags = default)
        {
            return builder.Add(new HealthCheckRegistration(
               name ?? "ProcessMonitor",
               sp => new ProcessMonitorHealthCheck(processName),
               failureStatus,
               tags));
        }
    }
}
