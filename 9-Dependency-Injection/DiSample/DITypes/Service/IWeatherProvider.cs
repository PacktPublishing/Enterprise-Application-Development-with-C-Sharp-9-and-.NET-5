using DITypes.Models;
using System.Collections.Generic;

namespace DITypes.Service
{
    public interface IWeatherProvider
    {
        IEnumerable<WeatherForecast> GetForecastOfLocation(string location);
    }
}
