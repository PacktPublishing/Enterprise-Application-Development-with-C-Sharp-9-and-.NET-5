using DITypes.Models;
using System.Collections.Generic;

namespace DITypes.Service
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecast> GetForeCast(string location);
    }
}