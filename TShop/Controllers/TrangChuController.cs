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

        public PartialViewResult ThuongHieuPartial()
        {
            //var loaiSanPham = db.LOAISANPHAMs.ToList();
            //ViewBag.vBLoaiSanPham = loaiSanPham;
            ViewBag.vBSanPhamBanChay = db.SANPHAMs.Where(n => n.LuotMua > 50 && n.Xoa == false).ToList();
            ViewBag.vBSanPhamMoi = db.SANPHAMs.Where(n => n.LuotMua > 50 && n.Xoa == false && n.Moi == true).ToList();

            return PartialView();
        }
       
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(KHACHHANG KH, FormCollection f)
        {
            if (KH == null)
            {
                return View();
            }
            else
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.Email == KH.Email);
                KHACHHANG kh1 = db.KHACHHANGs.SingleOrDefault(n => n.SoDienThoai == KH.SoDienThoai);
                if (kh == null && kh1 == null)
                {
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(KH.MatKhau);

                    KH.MatKhau = passwordHash;


                    db.KHACHHANGs.Add(KH);
                    db.SaveChanges();
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
            string passwordHash = KH.MatKhau;
            if (BCrypt.Net.BCrypt.Verify(tMK, passwordHash) == true)
            {
                Session["TaiKhoan"] = KH;
                return RedirectToAction("Index");
            }
            //if (KH != null)
            //{
            //    Session["TaiKhoan"] = KH;
            //    return RedirectToAction("Index");
            //}
            return RedirectToAction("Index");
        }

 
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index");
        }
    }
}