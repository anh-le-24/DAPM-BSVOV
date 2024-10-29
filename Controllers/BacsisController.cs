
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DoAnCNPM.Models;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace DoAnCNPM.Controllers;
using System.Collections;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;


public class BacsisController : Controller
{
    private readonly ILogger<BacsisController> _logger;

    public BacsisController(ILogger<BacsisController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
