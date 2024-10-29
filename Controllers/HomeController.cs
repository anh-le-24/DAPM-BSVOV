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

public class HomeController : Controller
{
    
    private readonly ILogger<HomeController> _logger;
    private readonly ISession _session;

    public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _session = httpContextAccessor.HttpContext.Session;
    }

    public IActionResult LayoutShare(){
        var taikhoan = _session.GetString("taikhoan");
        ViewData["TaiKhoan"] = taikhoan;

        DataModel db = new DataModel();
        ViewBag.listKB = db.get("EXEC getAllKhoaBenh");

        return View();
    }

    public IActionResult Index()
    {
        LayoutShare();

        return View();
    }
    public IActionResult Article(string MaBV)
    {
        LayoutShare();

        DataModel db = new DataModel();

        // Tạo danh sách tham số
        var parameters = new List<SqlParameter>
        {
            new SqlParameter("@MaBV", SqlDbType.Int) { Value = int.Parse(MaBV) }
        };

        // Sử dụng tham số hóa để tránh lỗi
        ViewBag.ListBV = db.getid("EXEC getBaiVietByIDMaKhoaBenh @MaBV", parameters);

        return View();
    }
    public IActionResult ListArticleLoaiBV(string MaLBV)
    {
        LayoutShare();

        DataModel db = new DataModel();

        // Tạo danh sách tham số
        var parameters = new List<SqlParameter>
        {
            new SqlParameter("@MaLBV", SqlDbType.Int) { Value = int.Parse(MaLBV) }
        };

        // Sử dụng tham số hóa để tránh lỗi
        ViewBag.ListLBV = db.getid("EXEC getBaiVietByIDMaLoai @MaLBV", parameters);

        return View();
    }
    public IActionResult ArticleLoaiBV(string MaBV)
    {
        LayoutShare();

        DataModel db = new DataModel();

        // Tạo danh sách tham số
        var parameters = new List<SqlParameter>
        {
            new SqlParameter("@MaBV", SqlDbType.Int) { Value = int.Parse(MaBV) }
        };

        // Sử dụng tham số hóa để tránh lỗi
        ViewBag.ListBV = db.getid("EXEC getBaiVietByID @MaBV", parameters);

        return View();
    }

    
    // ---------- ĐĂNG NHẬP ------------//
    public IActionResult Login()
    {
        LayoutShare();
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

     // -------- REGISTER ----------//
    public IActionResult Register()
    {
        LayoutShare();
        return View();
    }
    [HttpPost]
    public IActionResult RegisterProcess(string TenND, string Password, string sdt)
    {
        DataModel db = new DataModel();

        var list = db.get($"EXEC REGISTER N'{TenND}', '{Password}', '{sdt}'");

        if (list.Count > 0 && list[0] is ArrayList arrayList && arrayList.Count >= 2)
        {
            string userName = arrayList[0]?.ToString() ?? "Unknown";
            HttpContext.Session.SetString("taikhoan", userName);

            return RedirectToAction("Index", "Home");
        }
        else
        {
            ViewBag.Error = "Đăng ký không thành công. Vui lòng thử lại.";
            return RedirectToAction("Register", "Home");
        }
    }
    // ------- Action đăng xuất------- //
    public IActionResult Logout()
    {
        // Xóa toàn bộ session
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home");
    }
    public IActionResult FillterDoctor()
    {
        LayoutShare();
        DataModel db = new DataModel();
        ViewBag.ListNameDoc = db.get("SELECT MaBS, nd.TenND FROM BACSI bs, NGUOIDUNG nd where bs.MaND = nd.MaND");
        ViewBag.ListDoc = db.get("Exec getAllBacSi");
        
        return View();
    }
    public IActionResult FillterDoctorList(string khuvuc, string phikham, string khoabenh, string hocham)
    {
        LayoutShare();
        DataModel db = new DataModel();
        if(khuvuc != "Null"){
            khuvuc = "N'"+ khuvuc +"'";
        }
        if(hocham != "Null" ){
            hocham = "N'"+ hocham +"'";
        }
        ViewBag.ListDocFill = db.get($"EXEC FILTER_BACSI {khuvuc}, {phikham}, {khoabenh},  {hocham};");
        
        return View();
    }
    public IActionResult DetailDoctor(string MaBS)
    {
        LayoutShare();

        DataModel db = new DataModel();

        // Tạo danh sách tham số
        var parameters = new List<SqlParameter>
        {
            new SqlParameter("@MaBS", SqlDbType.Int) { Value = int.Parse(MaBS) }
        };

        // Sử dụng tham số hóa để tránh lỗi
        ViewBag.ListDBS = db.getid("EXEC DetDETAILlBACSI @MaBS", parameters);

        return View();
    }
    public IActionResult ListDoctor()
    {
        LayoutShare();
        
        return View();
    }

    // ----- PERSONAL PAGE ------//
     public IActionResult PersonalPage()
    {
        LayoutShare();
        DataModel db = new DataModel();
        var taikhoan = HttpContext.Session.GetString("taikhoan");
        ViewData["TaiKhoan"] = taikhoan;

        if (!string.IsNullOrEmpty(taikhoan))
        {
            var result = db.get("SELECT * from NGUOIDUNG where manD=" + taikhoan);
            if (result != null && result.Count > 0)
            {
                ViewBag.UserInfo = result[0]; // Lấy dòng đầu tiên của kết quả
            }
        }
        return View();
    }
    [HttpPost]
    public IActionResult UpdateUserInfo(string MaND, string TenND, string Email, 
                                        string NamSinh, string GioiTinh, 
                                        string DiaChi)
    {
        DataModel db = new DataModel();
        int manD = int.Parse(MaND);
        DateTime parsedDate = DateTime.Parse(NamSinh); 
        string formattedDate = parsedDate.ToString("yyyy-MM-dd");

        db.get($"EXEC UpdateUserInfo {manD}, N'{TenND}', '{Email}', '{formattedDate}', N'{GioiTinh}', N'{DiaChi}' ");

        return RedirectToAction("PersonalPage", "Home");
    }

    public IActionResult BookExamine()
    {
        LayoutShare();
        return View();
    }

     public IActionResult ExamineHistory()
    {
        LayoutShare();
        return View();
    }

    public IActionResult AccountBank()
    {
        LayoutShare();
        
        return View();
    }
    

    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
