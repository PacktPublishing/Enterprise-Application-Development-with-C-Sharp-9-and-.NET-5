using AuthSample.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSample.Controllers
{
    [Authorize(Policy = "Over14")]
    public class OrdersController : Controller
    {
        [AuthorizeAgeOver(18)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
