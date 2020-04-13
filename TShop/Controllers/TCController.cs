using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TShop.Models;
namespace TShop.Controllers
{
    public class TCController : Controller
    {
        TShopEntities db = new TShopEntities();

        public PartialViewResult ThuongHieuPartial()
        {
            //var loaiSanPham = db.LOAISANPHAMs.ToList();
            //ViewBag.vBLoaiSanPham = loaiSanPham;
            var NSX = db.NHASANXUATs.ToList();

            return PartialView(NSX);
        }
        public PartialViewResult DangKiNhanTinPartial()
        {
            return PartialView();
        }
        public PartialViewResult SanPhamBanChayPartial()
        {
            var SPBC = db.SANPHAMs.Where(n => n.LuotMua > 50 && n.Xoa == false).ToList();
            return PartialView(SPBC);
        }

        public PartialViewResult MenuPartial()
        {
            var DanhMucSanPhamCha = db.LOAISANPHAMs.Where(n => n.MaLSPGoc == null).ToList();
            //.Select(n => new LOAISANPHAM { MaLSP = n.MaLSP, TenLSP = n.TenLSP, MaLSPGoc = n.MaLSPGoc, CC = db.LOAISANPHAMs.Where(x => x.MaLSPGoc == n.MaLSP) }).ToList()
            //var DanhMucSanPham1 = db.LOAISANPHAMs.ToList();
            //ViewBag.vBDanhLoaiSanPham = DanhMucSanPham;
            return PartialView(DanhMucSanPhamCha);
        }
        public PartialViewResult SieuGiamGiaSanPhamPartial()
        {
            var SPBC = db.SANPHAMs.Where(n => n.LuotMua > 50 && n.Xoa==false&&n.SoLuongTon!=null ).ToList();
            return PartialView(SPBC);
        }
        public PartialViewResult LuaChonTotNhatPartial()
        {
            var LCTN = db.SANPHAMs.Where(n => n.LuotMua > 50 && n.Xoa == false).ToList();
            return PartialView(LCTN);
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