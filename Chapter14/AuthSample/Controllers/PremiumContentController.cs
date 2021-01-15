using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSample.Controllers
{
    [Authorize(Policy = "PremiumContentPolicy")]
    public class PremiumContentController : Controller
    {
        // GET: PremiumContentController
        public ActionResult Index()
        {
            return View();
        }
    }
}
