using DITypes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DITypes.Service
{
    public class WeatherService_PropertyInection : IWeatherService
    {
        private IWeatherProvider _weatherProvider;

        public IWeatherProvider WeatherProvider
        {
            get => _weatherProvider == null ?
                        throw new InvalidOperationException("WeatherService is not initialized")
                    : _weatherProvider;
            set => _weatherProvider = value;
        }


        public IEnumerable<WeatherForecast> GetForeCast(string location) =>
            this.WeatherProvider.GetForecastOfLocation(location);
    }
}
