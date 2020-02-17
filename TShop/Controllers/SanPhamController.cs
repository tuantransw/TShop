using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TShop.Models;
namespace TShop.Controllers
{
    public class SanPhamController : Controller
    {
        TShopEntities db = new TShopEntities();
        

        public ActionResult ChiTietSanPham(int? MaSP)
        {
            if(MaSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                
            }
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == MaSP && n.Xoa == false);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }
        public ActionResult SanPhamTheoLSP(int? MaLSP)
        {
            if (MaLSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            var sp = db.SANPHAMs.Where(n => n.MaLSP == MaLSP && n.Xoa == false).ToList();
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }
    }
}