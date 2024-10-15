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

public class HomeController : Controller
{
    
    private readonly ILogger<HomeController> _logger;
    private readonly ISession _session;

    public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _session = httpContextAccessor.HttpContext.Session;
    }

    public IActionResult Index()
    {
        var taikhoan = _session.GetString("taikhoan");
        ViewData["TaiKhoan"] = taikhoan;

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
    
    // ---------- ĐĂNG NHẬP ------------//
    public IActionResult Login()
    {
        DataModel db = new DataModel();
        ViewBag.listKB = db.get("EXEC getAllKhoaBenh");
        return View();
    }
    [HttpPost]
    public IActionResult LoginProcess(string sdt, string password)
    {
        DataModel db = new DataModel();
        var list = db.get("EXEC CheckLogin '" + sdt + "','" + password + "'");

        if (list.Count > 0 && list[0] is ArrayList arrayList && arrayList.Count > 1)
        {
            var userInfo = arrayList[0]?.ToString() ?? "Unknown";
            _session.SetString("taikhoan", userInfo);
            return RedirectToAction("Index", "Home");
        }
        else
        {
            return RedirectToAction("Login", "Home");
        }
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
    public IActionResult ListDoctor()
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

    public IActionResult BookExamine()
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
