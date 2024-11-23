
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
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;


public class BacsisController : Controller
{
    private readonly ILogger<BacsisController> _logger;
    private readonly ISession _session;
    public BacsisController(ILogger<BacsisController> logger,IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _session = httpContextAccessor.HttpContext.Session;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult KTBS(string TenDn, string password)
    {
        DataModel db = new DataModel();
        var list = db.get("EXEC CheckLoginBS '" + TenDn + "','" + password + "'");
        if (list.Count > 0 && list[0] is ArrayList arrayList && arrayList.Count > 1)
        {
            var userId = arrayList[0]?.ToString() ?? "Unknown"; // Giả sử ID là phần tử thứ 2 trong ArrayList
            _session.SetString("userId", userId); // Lưu ID vào session
            return RedirectToAction("HomeBs", "Bacsis");
        }
        else
        {
            return RedirectToAction("Index", "Bacsis");
        }
    }


    public IActionResult DangKyBs(){
        return View();
    }

    [HttpPost]
    public IActionResult ThucHienDKBs(
        string username,
        string password,
        string email,
        string phone,
        string diaChi,
        DateTime birthyear,
        string gender,
        IFormFile hinhanh, // Sử dụng IFormFile thay vì HttpPostedFileBase
        string cmtnumber,
        DateTime issuedate,
        string issueplace,
        IFormFile cmtimagefront,
        IFormFile cmtimageback,
        string chonChuyenKhoa,
        int namKinhNghiem,
        string noiCongTac,
        string khoa,
        string soChungChi,
        DateTime ngayThangCap,
        IFormFile anhChungChi1,
        IFormFile anhChungChi2,
        string hocVi,
        string hocHam,
        string chuyenMon,
        string thontinbangcap,
        string gioithieu,
        string cacBenhDieuTri,
        string quaTrinhhoc,
        string quaTrinhCongTac,
        string nghienCuuKhoaHoc,
        string giangDay,
        string hoiVienCongTac)
    {
        try
        {
            DataModel db = new DataModel();
            // Kiểm tra tệp hình ảnh và lưu vào thư mục "Hinh"
            if (hinhanh != null && hinhanh.Length > 0)
            {
                string filename = Path.GetFileName(hinhanh.FileName); // Lấy tên tệp
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", filename); // Đường dẫn lưu tệp
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    hinhanh.CopyTo(stream); // Lưu tệp
                }
            }

            // Tương tự cho các tệp khác như cmtimagefront, cmtimageback, anhChungChi1, anhChungChi2
            // Lưu tên tệp vào cơ sở dữ liệu
            if (cmtimagefront != null && cmtimagefront.Length > 0)
            {
                string cmtFrontFileName = Path.GetFileName(cmtimagefront.FileName);
                string cmtFrontPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", cmtFrontFileName);
                using (var stream = new FileStream(cmtFrontPath, FileMode.Create))
                {
                    cmtimagefront.CopyTo(stream);
                }
            }

            if (cmtimageback != null && cmtimageback.Length > 0)
            {
                string cmtBackFileName = Path.GetFileName(cmtimageback.FileName);
                string cmtBackPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", cmtBackFileName);
                using (var stream = new FileStream(cmtBackPath, FileMode.Create))
                {
                    cmtimageback.CopyTo(stream);
                }
            }

            int randomValue = new Random().Next(1, 10000);

            // Sử dụng stored procedure để thực hiện thêm thông tin bác sĩ vào cơ sở dữ liệu
            string result = "EXEC DangKyBacSi N'" + username + "','"
                + password + "', '" 
                + email + "', '" 
                + phone + "', N'"
                + diaChi + "', '" 
                + birthyear.ToString("yyyy-MM-dd") + "', N'" 
                + gender + "', N'"
                + hinhanh.FileName + "', 0 , "
                + randomValue +", NULL, 2 ,"
                + cmtnumber + ", '"
                + issuedate.ToString("yyyy-MM-dd") + "', N'"
                + issueplace + "', N'"         
                + cmtimagefront.FileName + "', '" 
                + cmtimageback.FileName + "', '" 
                + chonChuyenKhoa + "', " 
                + namKinhNghiem + ", "
                + noiCongTac + ", " 
                + khoa + ", '"
                + soChungChi + "', '" 
                + ngayThangCap.ToString("yyyy-MM-dd") + "', N'"
                + anhChungChi1.FileName + "', N'" 
                + anhChungChi2.FileName + "', N'" 
                + hocVi + "', N'" 
                + hocHam + "', N'"
                + chuyenMon + "', N'"
                + thontinbangcap + "', N'" 
                + gioithieu + "', N'"
                + cacBenhDieuTri + "', N'"
                + quaTrinhhoc + "', N'" 
                + quaTrinhCongTac + "', N'"
                + nghienCuuKhoaHoc + "', N'"
                + giangDay + "', N'"
                + hoiVienCongTac + "', 0 ,2 ;";

            db.get(result);         

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Có lỗi xảy ra khi thực hiện đăng ký bác sĩ.");
            // Nếu có lỗi, giữ lại trang DangKyBs và thông báo lỗi
            return RedirectToAction("DangKyBs", "Bacsis");
        }   
        return RedirectToAction("DKTC", "Bacsis");
  
    }


    public IActionResult HomeBs(){
        return View();  
    }

    public IActionResult DKTC(){
        return View(); 
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
    public IActionResult HoSoBN()
    {
        DataModel db = new DataModel();
        ViewBag.list = db.get("exec GetAllPatientRecords");
        return View();
    }

    [HttpPost]
    public IActionResult ThemHs(string patientPhone, string patientDescription)
    {
        DataModel db = new DataModel();
        ViewBag.list = db.get("exec AddPatientRecordByPhone '"+ patientPhone +"',N'" + patientDescription + "'");
        return RedirectToAction("HoSoBN", "Bacsis");
    }
    public IActionResult XoaHs(string id){
        DataModel db = new DataModel();
        ViewBag.list=db.get("EXEC DeletePatientRecord "+ id);
        return RedirectToAction("HoSoBN", "Bacsis");
    }

    [HttpPost]
    public IActionResult Suahoso(string patientId,string patientDescription){
        DataModel db = new DataModel();
        ViewBag.list=db.get("EXEC UpdatePatientRecord "+ patientId +",N'"+ patientDescription + "'");
        return RedirectToAction("HoSoBN", "Bacsis");
    }

}
