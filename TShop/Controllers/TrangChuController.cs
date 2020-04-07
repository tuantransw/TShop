using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TShop.Models;

namespace TShop.Controllers
{
    public class TrangChuController : Controller
    {
        TShopEntities db = new TShopEntities();
        public ActionResult Index()
        {
            //var loaiSanPham = db.LOAISANPHAMs.ToList();
            //ViewBag.vBLoaiSanPham = loaiSanPham;
            ViewBag.vBSanPhamBanChay = db.SANPHAMs.Where(n => n.LuotMua > 50 && n.Xoa == false).ToList();
            ViewBag.vBSanPhamMoi = db.SANPHAMs.Where(n => n.LuotMua > 50 && n.Xoa == false&& n.Moi == true).ToList();
            
            return View();
        }

       
        [HttpGet]
        public ActionResult TaiKhoan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TaiKhoan(KHACHHANG KH, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.Email == KH.Email);
                KHACHHANG kh1 = db.KHACHHANGs.SingleOrDefault(n => n.SoDienThoai == KH.SoDienThoai);
                if (kh == null && kh1 == null)
                {
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(KH.MatKhau);

                    KH.MatKhau = passwordHash;

                    
                    db.KHACHHANGs.Add(KH);
                    db.SaveChanges();
                    ViewBag.ThongBao = "Tạo tài khoản thành công";
                    Session["TaiKhoan"] = KH;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ThongBao = "Số điện thoại hoặc email đã được sử dụng";
                    return View();
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string tTDN = f["textTDN"].ToString();
            string tMK = f["textMK"].ToString();
       
            KHACHHANG KH = db.KHACHHANGs.SingleOrDefault(n => n.Email == tTDN);
            if (KH == null)
            {
                ViewBag.ThongBao1 = "Tên tài khoản hoặc mật khẩu không chính xác";
                return View("TaiKhoan");
            }
            string passwordHash = KH.MatKhau;
            if (BCrypt.Net.BCrypt.Verify(tMK, passwordHash) == true)
            {
                Session["TaiKhoan"] = KH;
          
                return RedirectToAction("Index");
            }
            ViewBag.ThongBao1 = "Tên tài khoản hoặc mật khẩu không chính xác";
            return View("TaiKhoan");
        }

 
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult abc()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult abc(KHACHHANG KH)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}