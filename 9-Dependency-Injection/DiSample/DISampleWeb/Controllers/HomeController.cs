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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedService scopedService;
        private readonly IScopedService scopedService2;
        private readonly ISingletonService singletonService;
        private readonly ITransientService transientService;
        private readonly ITransientService transientService2;

        public HomeController(ILogger<HomeController> logger,
                            IScopedService scopedService,
                            IScopedService scopedService2,
                            ISingletonService singletonService,
                            ITransientService transientService,
                            ITransientService transientService2,
                            IEnumerable<IWeatherForcastService> weatherForcastService)
        {
            _logger = logger;
            this.scopedService = scopedService;
            this.scopedService2 = scopedService2;
            this.singletonService = singletonService;
            this.transientService = transientService;
            this.transientService2 = transientService2;
        }

        public IActionResult Index([FromServices] ISingletonService singletonService2)
        {
            var viewModel = new HomeViewModel
            {
                Scoped = scopedService.GetHashCode(),
                Scoped2 = scopedService2.GetHashCode(),
                Singleton = singletonService.GetHashCode(),
                Transient = transientService.GetHashCode(),
                Transient2 = transientService2.GetHashCode(),
            };

            //Use the singleton service which is injected via methos injection
            var singletonObjectHashCode = singletonService2.GetHashCode();
            
            return View(viewModel);
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
