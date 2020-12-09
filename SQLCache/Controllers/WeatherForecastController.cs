using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLCache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IDistributedCache distributedCache;

        public WeatherForecastController(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        [HttpGet]
        public IActionResult Get()
        {
            DateTime? cacheEntry;
            if (distributedCache.Get("Weather") == null)
            {
                cacheEntry = DateTime.Now;
                var cacheEntryOptions = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(5))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));
                distributedCache.SetString("Weather", cacheEntry.ToString(), cacheEntryOptions);
            }
            var cachedDate = distributedCache.GetString("Weather");
            var rng = new Random();
            return Ok(from temp in Enumerable.Range(1, 5)
                      select new
                      {
                          Date = cachedDate,
                          TemperatureC = rng.Next(-20, 55),
                          Summary = "Rainy day"
                      });
        }
    }
}
