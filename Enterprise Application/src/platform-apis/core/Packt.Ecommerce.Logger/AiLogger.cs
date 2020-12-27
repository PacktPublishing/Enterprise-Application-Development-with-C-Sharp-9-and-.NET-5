using Logger;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;


namespace Packt.Ecommerce.Logger
{
    public class AiLogger : ICustomLogger
    {
        private TelemetryClient client;
        
        public AiLogger(TelemetryClient client)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            this.client = client;


        }

        public void Dependency(string dependencyTypeName, string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, bool success)
        {
            this.client.TrackDependency(dependencyTypeName, dependencyName, data, startTime, duration, success);
        }

        public void Warning(string message, IDictionary<string, string> properties = null)
        {
            this.client.TrackTrace(message, SeverityLevel.Warning, properties);
        }

        public void Error(string message, IDictionary<string, string> properties = null)
        {
            this.client.TrackTrace(message, SeverityLevel.Error, properties);
        }

        public void Event(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            this.client.TrackEvent(eventName, properties, metrics);
        }

        public void Metric(string name, long value, IDictionary<string, string> properties = null)
        {

            this.client.TrackMetric(name, value, properties);

        }

        public void Exception(Exception exception, IDictionary<string, string> properties = null)
        {
            this.client.TrackException(exception, properties);
        }

        public void Information(string message, IDictionary<string, string> properties = null)
        {
            this.client.TrackTrace(message, SeverityLevel.Information, properties);
        }

        public void Request(string name, DateTimeOffset startTime, TimeSpan duration, string responseCode, bool success)
        {
            this.client.TrackRequest(name, startTime, duration, responseCode, success);
        }

        public void Verbose(string message, IDictionary<string, string> properties = null)
        {
            this.client.TrackTrace(message, SeverityLevel.Verbose, properties);
        }
    }
}
