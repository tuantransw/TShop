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
    }
}