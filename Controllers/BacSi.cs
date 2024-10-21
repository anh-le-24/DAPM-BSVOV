using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DoAnCNPM.Controllers
{
    [Route("[bacsi]")]
    public class BacSi : Controller
    {
        private readonly ILogger<BacSi> _logger;

        public BacSi(ILogger<BacSi> logger)
        {
            _logger = logger;
        }

        public IActionResult HomeBS()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}