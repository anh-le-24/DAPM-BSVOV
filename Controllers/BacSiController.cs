using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DoAnCNPM.Controllers
{
    // Đặt route cho controller
    [Route("bacsi")] 
    public class BacSiController : Controller
    {
        private readonly ILogger<BacSiController> _logger;

        public BacSiController(ILogger<BacSiController> logger)
        {
            _logger = logger;
        }

        [HttpGet] // Chỉ định đây là một action GET
        public IActionResult Index()
        {
            return View("BacSi/BacSiQL"); 
        }

        // Xử lý lỗi
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error"); // Đảm bảo sử dụng view Error hợp lệ
        }
    }
}
