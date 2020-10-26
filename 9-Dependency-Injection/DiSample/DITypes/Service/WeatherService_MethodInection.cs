using DITypes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DITypes.Service
{
    public class WeatherService_MethodInection
    {
        public IEnumerable<WeatherForecast> GetForeCast(string location, IWeatherProvider weatherProvider)
        {
            if (weatherProvider == null)
            {
                throw new ArgumentNullException(nameof(weatherProvider));
            }
            return weatherProvider.GetForecastOfLocation(location);
        }
    }
}
