using System;
using System.Collections.Generic;

namespace Packt.Ecommerce.Logger
{
    public interface ILogger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencyTypeName"></param>
        /// <param name="dependencyName"></param>
        /// <param name="data"></param>
        /// <param name="startTime"></param>
        /// <param name="duration"></param>
        /// <param name="success"></param>
        void Dependency(string dependencyTypeName, string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, bool success);

        /// <summary>
        /// Writes an Error Log Message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="properties"></param>
        void Error(string message, IDictionary<string, string> properties = null);

        void Event(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);

        void Metric(string name, long value, IDictionary<string, string> properties = null);

        void Exception(Exception exception, IDictionary<string, string> properties = null);

        void Information(string message, IDictionary<string, string> properties = null);

        void Request(string name, DateTimeOffset startTime, TimeSpan duration, string responseCode, bool success);

        void Verbose(string message, IDictionary<string, string> properties = null);

        void Warning(string message, IDictionary<string, string> properties = null);
    }
}
