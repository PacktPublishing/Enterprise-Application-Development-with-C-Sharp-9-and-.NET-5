using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IsolationStorageHO.Models;
using System.IO.IsolatedStorage;
using System.IO;

namespace IsolationStorageHO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            Lazy<ImageFile> imageFile = new Lazy<ImageFile>(() => new ImageFile("test"));

            IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            if (isolatedStorageFile.FileExists("personilization.txt"))
            {
                using (IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream("personilization.txt", FileMode.Open, isolatedStorageFile))
                {   
                    using (StreamReader streamReader = new StreamReader(isolatedStorageFileStream))
                    {
                        Console.WriteLine(streamReader.ReadToEnd());
                    }
                }
            }
            else
            {
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("TestStore.txt", FileMode.CreateNew, isolatedStorageFile))
                {
                    using (StreamWriter writer = new StreamWriter(isoStream))
                    {
                        writer.WriteLine("Hello Isolated Storage");
                        Console.WriteLine("You have written to the file.");
                    }
                }
            }
            return View();
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
