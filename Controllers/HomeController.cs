using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DoAnCNPM.Models;

namespace DoAnCNPM.Controllers;

public class HomeController : Controller
{
    
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        DataModel db = new DataModel();
        ViewBag.listKB = db.get("EXEC getAllKhoaBenh");
        
        return View();
    }
    public IActionResult Article()
    {
        DataModel db = new DataModel();
        ViewBag.listKB = db.get("EXEC getAllKhoaBenh");

        return View();
    }
    public IActionResult Login()
    {
        DataModel db = new DataModel();
        ViewBag.listKB = db.get("EXEC getAllKhoaBenh");
        return View();
    }
    public IActionResult FillterDoctor()
    {
        DataModel db = new DataModel();
        ViewBag.listKB = db.get("EXEC getAllKhoaBenh");
        
        return View();
    }
    public IActionResult DetailDoctor()
    {
        DataModel db = new DataModel();
        ViewBag.listKB = db.get("EXEC getAllKhoaBenh");
        
        return View();
    }
    public IActionResult Register()
    {
        DataModel db = new DataModel();
        ViewBag.listKB = db.get("EXEC getAllKhoaBenh");
        return View();
    }
     public IActionResult PersonalPage()
    {
        DataModel db = new DataModel();
        ViewBag.listKB = db.get("EXEC getAllKhoaBenh");
        return View();
    }
     public IActionResult ExamineHistory()
    {
        DataModel db = new DataModel();
        ViewBag.listKB = db.get("EXEC getAllKhoaBenh");
        return View();
    }

    public IActionResult AccountBank()
    {
        DataModel db = new DataModel();
        ViewBag.listKB = db.get("EXEC getAllKhoaBenh");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
