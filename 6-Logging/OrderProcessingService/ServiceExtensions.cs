using Logger;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProcessingService
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
        {
            string instrumentationkey = configuration.GetValue<string>("Telemetry:InstrumentationKey");
            var aiOptions = new ApplicationInsightsServiceOptions
            {
                InstrumentationKey = instrumentationkey,
                EnableAdaptiveSampling = false,
                EnableAppServicesHeartbeatTelemetryModule = true,
                EnablePerformanceCounterCollectionModule = true,
                EnableRequestTrackingTelemetryModule = true,
                EnableDependencyTrackingTelemetryModule = true
            };

            services.AddApplicationInsightsTelemetry(aiOptions);
           
            return services.AddScoped<ILogger, AiLogger>();
        }

        public static ILogger GetLogger(this IServiceCollection services)
        {
            var intermediateServiceProvider = services.BuildServiceProvider();
            var logger = intermediateServiceProvider.GetService<ILogger>();
            return logger;
        }
    }
}
