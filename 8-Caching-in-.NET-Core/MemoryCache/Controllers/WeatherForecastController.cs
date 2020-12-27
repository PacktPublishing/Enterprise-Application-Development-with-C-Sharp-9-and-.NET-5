using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MemoryCache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IMemoryCache cache;

        public WeatherForecastController(IMemoryCache cache)
        {
            this.cache = cache;
        }

        [HttpGet]
        public IActionResult Get()
        {
            DateTime? cacheEntry;
            if (!cache.TryGetValue("Weather", out cacheEntry))
            {
                cacheEntry = DateTime.Now;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(5))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10))
                    .SetPriority(CacheItemPriority.NeverRemove);
                cacheEntryOptions.RegisterPostEvictionCallback(EvictionCallback);
                cache.Set("Weather", cacheEntry, cacheEntryOptions);
            }
            cache.TryGetValue("Weather", out cacheEntry);
            var rng = new Random();
            return Ok(from temp in Enumerable.Range(1, 5)
                      select new
                      {
                          Date = cacheEntry,
                          TemperatureC = rng.Next(-20, 55),
                          Summary = "Rainy day"
                      });
        }

        [HttpGet("{Id}")]
        [ResponseCache(CacheProfileName = "Default")]
        public IEnumerable<WeatherForecast> Get([FromRoute] int Id = 0)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = "Hello"
            }).ToArray();
        }
        private void EvictionCallback(object key, object value, EvictionReason reason, object state)
        {
            Debug.WriteLine(reason);
        }

    }
}
