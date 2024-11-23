using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DoAnCNPM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


public class KhachHangAPI : Controller
{
    public JsonResult Index()
    {
        DataModel db =new DataModel();
        ArrayList a = db.get("EXEC getAllBenhVien");
        return Json(a);
    }
    public JsonResult KhoaBenh()
    {
        DataModel db =new DataModel();
        ArrayList a = db.get("EXEC getAllKhoaBenh");
        return Json(a);
    }
    public JsonResult BacSi()
    {
        DataModel db =new DataModel();
        ArrayList a = db.get("EXEC getAllBacSi");
        return Json(a);
    }
    [HttpPost]
    public JsonResult DangNhap(string sdt, string password)
    {
        DataModel db = new DataModel();
        string query = $"EXEC CheckLogin '{sdt}', '{password}'";
        ArrayList result = db.get(query);

        if (result.Count > 0)
        {
            return Json(new { success = true, data = result });
        }
        else
        {
            return Json(new { success = false, message = "Số điện thoại hoặc mật khẩu không đúng." });
        }
    }
        

}
