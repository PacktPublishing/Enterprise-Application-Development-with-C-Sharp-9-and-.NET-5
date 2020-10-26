using DITypes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DITypes.Service
{
    public class WeatherService_ConstructorInjection : IWeatherService
    {
        private readonly IWeatherProvider weatherProvider;
        public WeatherService_ConstructorInjection(IWeatherProvider weatherProvider)
                => this.weatherProvider = weatherProvider;

        public IEnumerable<WeatherForecast> GetForeCast(string location) =>
            this.weatherProvider.GetForecastOfLocation(location);


    }
}
