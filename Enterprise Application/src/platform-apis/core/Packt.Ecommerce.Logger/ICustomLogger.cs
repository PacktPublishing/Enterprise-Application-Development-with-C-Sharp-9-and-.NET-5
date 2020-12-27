using System;
using System.Collections.Generic;

namespace Logger
{
    public interface ICustomLogger
    {
        /// <summary>
        /// Logs dependency related message
        /// </summary>
        /// <param name="dependencyTypeName">Dependency Type Name</param>
        /// <param name="dependencyName">Dependency Name</param>
        /// <param name="data">Dependency data</param>
        /// <param name="startTime">Dependency call start time</param>
        /// <param name="duration">Duration of Dependency call</param>
        /// <param name="success">Dependency call succeeded or failed</param>
        void Dependency(string dependencyTypeName, string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, bool success);

        /// <summary>
        /// Logs Error message
        /// </summary>
        /// <param name="message">Error Log Message</param>
        /// <param name="properties">Error Log Message properties</param>
        void Error(string message, IDictionary<string, string> properties = null);

        /// <summary>
        /// Logs Event related messages
        /// </summary>
        /// <param name="eventName">Event Name</param>
        /// <param name="properties">Event properties</param>
        /// <param name="metrics">Event metrics</param>
        void Event(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);

        /// <summary>
        /// Logs Metric related messages
        /// </summary>
        /// <param name="name">Metric Name</param>
        /// <param name="value">time or duration</param>
        /// <param name="properties">Metric properties</param>        
        void Metric(string name, long value, IDictionary<string, string> properties = null);

        /// <summary>
        /// Logs Exception message
        /// </summary>
        /// <param name="exception">Exception Log Message</param>
        /// <param name="properties">Exception Log Message properties</param>
        void Exception(Exception exception, IDictionary<string, string> properties = null);

        /// <summary>
        /// Logs Information message
        /// </summary>
        /// <param name="message">Information Log Message</param>
        /// <param name="properties">Information Log Message properties</param>
        void Information(string message, IDictionary<string, string> properties = null);


        /// <summary>
        /// Logs custom request message
        /// </summary>
        /// <param name="name">request name</param>
        /// <param name="startTime">request startTime</param>
        /// <param name="duration">request duration</param>
        /// <param name="responseCode">responseCode for the request</param>
        /// <param name="success">request succeeded or failed</param>
        void Request(string name, DateTimeOffset startTime, TimeSpan duration, string responseCode, bool success);


        /// <summary>
        /// Logs Verbose message
        /// </summary>
        /// <param name="message">Verbose Log Message</param>
        /// <param name="properties">Verbose Log Message properties</param>
        void Verbose(string message, IDictionary<string, string> properties = null);

        /// <summary>
        /// Logs Warning message
        /// </summary>
        /// <param name="message">Warning Log Message</param>
        /// <param name="properties">Warning Log Message properties</param>
        void Warning(string message, IDictionary<string, string> properties = null);
    }
}
