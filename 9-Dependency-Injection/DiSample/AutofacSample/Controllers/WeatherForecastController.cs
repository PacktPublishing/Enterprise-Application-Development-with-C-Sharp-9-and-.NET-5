using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutofacSample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AutofacSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherProvider weatherProvider;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherProvider weatherProvider)
        {
            _logger = logger;
            this.weatherProvider = weatherProvider;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return weatherProvider.GetForecast();
        }
    }
}
