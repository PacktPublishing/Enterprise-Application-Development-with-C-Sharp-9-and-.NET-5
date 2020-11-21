// "//-----------------------------------------------------------------------".
// <copyright file="ProcessMonitorHealthCheck.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Common.HealthCheck
{
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Diagnostics.HealthChecks;

    /// <summary>
    /// Custom health check for process monitor.
    /// </summary>
    public class ProcessMonitorHealthCheck : IHealthCheck
    {
        /// <summary>
        /// Field to hold process name.
        /// </summary>
        private readonly string processName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessMonitorHealthCheck"/> class.
        /// </summary>
        /// <param name="processName">Name of the process to monitor.</param>
        public ProcessMonitorHealthCheck(string processName) => this.processName = processName;

        /// <summary>
        /// Check health.
        /// </summary>
        /// <param name="context">Health check context.</param>
        /// <param name="cancellationToken">cancellation token.</param>
        /// <returns>Health Check result.</returns>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Process[] pname = Process.GetProcessesByName(this.processName);
            if (pname.Length == 0)
            {
                return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, description: $"Process with the name {this.processName} is not running."));
            }
            else
            {
                return Task.FromResult(HealthCheckResult.Healthy());
            }
        }
    }
}
