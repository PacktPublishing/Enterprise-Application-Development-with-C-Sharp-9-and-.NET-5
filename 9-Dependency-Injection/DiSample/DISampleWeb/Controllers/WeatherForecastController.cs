using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DISampleWeb.Models;
using DISampleWeb.Services;

namespace DISampleWeb.Controllers
{
    public class WeatherForecastController : Controller
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEnumerable<IWeatherForcastService> weatherForcastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
                                            IEnumerable<IWeatherForcastService> weatherForcastService)
        {
            _logger = logger;
            this.weatherForcastService = weatherForcastService;
        }

      
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
