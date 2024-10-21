using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DoAnCNPM.Views.BacSi
{
    public class HomeBS : PageModel
    {
        private readonly ILogger<HomeBS> _logger;

        public HomeBS(ILogger<HomeBS> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}