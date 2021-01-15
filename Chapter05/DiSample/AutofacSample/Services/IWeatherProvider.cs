using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutofacSample.Services
{
    public interface IWeatherProvider
    {
        IEnumerable<WeatherForecast> GetForecast();
    }
}
