using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DoAnCNPM.Models;

namespace DAPMBSVOV.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IActionResult Index()
        {
            DataModel db = new DataModel();
            return View(); // Trả về view 'Index.cshtml'
        }
        public IActionResult DMBaiViet()
        {
            DataModel db = new DataModel();
            ViewBag.listLBV = db.get("SELECT * from LOAIBAIVIET");
            ViewBag.listBV = db.get("SELECT * from BAIVIET");
            ViewBag.listKB = db.get("SELECT * from KHOABENH");
            return View(); 
        }
        [HttpPost]
        public IActionResult ThemBaiViet(string tieudebv, 
                                        string noidungbv, 
                                        string makb, 
                                        string linkytb, 
                                        string ngaydang, 
                                        string malbv, 
                                        string luotxem)
        {
            DataModel db = new DataModel();
            ViewBag.list = db.get("EXEC AddBAIVIET N'" +tieudebv+ "', N'" +noidungbv+ "'," +makb+ ",'" +linkytb+ "','" +ngaydang+ "'," +malbv+ "," +luotxem+ ";");
            return RedirectToAction("DMBaiViet", "Admin"); 
        }

        [HttpPost]
        public IActionResult XoaBaiViet(string id)
        {
            DataModel db = new DataModel();
            ViewBag.list = db.get("EXEC DeleteBAIVIET " + id + ";");
            return RedirectToAction("DMBaiViet", "Admin"); 
        }

        public IActionResult TimBaiViet(string mabv)
        {
            DataModel db = new DataModel();
            ViewBag.list = db.get("EXEC FindBAIVIET_ID " +mabv+ ";");            
            ViewBag.listLBV = db.get("SELECT * from LOAIBAIVIET");
            ViewBag.listKB = db.get("SELECT * from KHOABENH");
            return View(); 
        }

        public IActionResult ChiTietBaiViet(string mabv)
        {
            DataModel db = new DataModel();
            ViewBag.list = db.get("EXEC FindBAIVIET_ID " +mabv+ ";");      
            ViewBag.listLBV = db.get("SELECT * from LOAIBAIVIET");
            ViewBag.listKB = db.get("SELECT * from KHOABENH");      
            return View(); 
        }

        [HttpPost]
        public IActionResult SuaBaiViet(string mabv,
                                        string tieudebv, 
                                        string noidungbv, 
                                        string makb, 
                                        string linkytb, 
                                        string ngaydang, 
                                        string malbv, 
                                        string luotxem)
        {
            DataModel db = new DataModel();
            ViewBag.list = db.get("EXEC UpdateBAIVIET " +mabv+ ", N'" +tieudebv+ "', N'" +noidungbv+ "'," +makb+ ",'" +linkytb+ "','" +ngaydang+ "'," +malbv+ "," +luotxem+ ";");
            return RedirectToAction("DMBaiViet", "Admin"); 
        }

        public IActionResult DMChuyenNganh()
        {
            DataModel db = new DataModel();
            ViewBag.listCN = db.get("SELECT * from CHUYENNGANH");
            return View(); 
        }

        [HttpPost]
        public IActionResult ThemChuyenNganh(string tencn, IFormFile hinhanh)
        {
            DataModel db = new DataModel();
            //lấy tên tệp
            string namefile = Path.GetFileName(hinhanh.FileName);

            //Đường dẫn để lưu tệp
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");
            Directory.CreateDirectory(uploadsFolder); //Tạo thư mục nếu chưa tồn tại
            string filePath = Path.Combine(uploadsFolder, namefile);
            //Lưu tệp
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                hinhanh.CopyTo(stream);
            }
            db.get("EXEC sp_ThemChuyenNganh N'" + tencn + "'," + hinhanh + ";");
            return RedirectToAction("DMChuyenNganh", "Admin");
        }

        public IActionResult DMKhoaBenh()
        {
            DataModel db = new DataModel();
            ViewBag.listKB = db.get("SELECT * from KHOABENH");
            return View(); 
        }

        [HttpPost]
        public IActionResult ThemKhoaBenh(string tenkb, string mota)
        {
            DataModel db = new DataModel();
            ViewBag.list = db.get("EXEC sp_ThemKhoaBenh N'" + tenkb + "', N'" + mota + "';");
            return RedirectToAction("DMKhoaBenh", "Admin");
        }

        [HttpPost]
        public IActionResult XoaKhoaBenh(string id)
        {
            DataModel db = new DataModel();
            ViewBag.list = db.get("EXEC sp_XoaKhoaBenh " + id);
            return RedirectToAction("DMKhoaBenh", "Admin"); 
        }

        public IActionResult DMBenhVien()
        {
            DataModel db = new DataModel();
            ViewBag.list = db.get("SELECT * from BENHVIEN");
            return View();
        }

        [HttpPost] 
        public IActionResult ThemBenhVien(string tenbv, IFormFile hinhanh)
        {
            DataModel db = new DataModel();
            //lấy tên tệp
            string namefile = Path.GetFileName(hinhanh.FileName);

            //Đường dẫn để lưu tệp
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");
            Directory.CreateDirectory(uploadsFolder); //Tạo thư mục nếu chưa tồn tại
            string filePath = Path.Combine(uploadsFolder, namefile);
            //Lưu tệp
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                hinhanh.CopyTo(stream);
            }
            db.get("EXEC AddBenhVien N'" + tenbv + "'," + hinhanh.FileName + ";");
            return RedirectToAction("DMBenhVien", "Admin");
        }

        public IActionResult HoSoBenhNhan(string mapq)
        {
            DataModel db = new DataModel();
            ViewBag.list = db.get("EXEC GetPatientsByRole " +2+ ";");
            return View();
        }

        public IActionResult ChiTietBenhNhan(string id)
        {
            DataModel db = new DataModel();
            ViewBag.list = db.get("EXEC GetPatientDetailsByUser_Id " +id+ ";" );
            return View();
        }

        public IActionResult HoSoBacSi(string mapq)
        {
            DataModel db = new DataModel();
            ViewBag.list = db.get("EXEC GetDoctorDetailsByRoleId " +3+ ";");
            return View();
        }

        public IActionResult ChiTietBacSi(string id)
        {
            DataModel db = new DataModel();
            ViewBag.list = db.get("EXEC GetDoctorDetailsByUser_Id " +id+ ";" );
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(); // Trả về view lỗi mặc định 'Error.cshtml'
        }
    }
}