using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DITypes.Models;
using DITypes.Service;

namespace DITypes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherService weatherService;

        public HomeController(ILogger<HomeController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            this.weatherService = weatherService;
        }

        public IActionResult Constructor()
        {
            return View(weatherService.GetForeCast("Hyderabad"));
        }

        public IActionResult Property()
        {
            WeatherService_PropertyInection weatherService = new WeatherService_PropertyInection();
            weatherService.WeatherProvider = new WeatherProvider();
            return View(weatherService.GetForeCast("Hyderabad"));
        }

        public IActionResult Method()
        {
            WeatherService_MethodInection weatherService = new WeatherService_MethodInection();
            var weatherProvider = new WeatherProvider();
            return View(weatherService.GetForeCast("Hyderabad", weatherProvider));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
