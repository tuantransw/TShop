using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TShop.Models;
namespace TShop.Controllers
{
    public class QuanLyPhieuNhapController : Controller
    {
        TShopEntities db = new TShopEntities();
        // GET: QuanLyPhieuNhap

        public ActionResult NhapHang()
        {
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.ListSP = db.SANPHAMs;
            return View();
        }
        [HttpPost]
        public ActionResult NhapHang(IEnumerable<CHITIETPHIEUNHAP> Model)
        {
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