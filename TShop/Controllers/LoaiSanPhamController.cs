using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TShop.Models;

namespace TShop.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        TShopEntities db = new TShopEntities();
   
        public ActionResult DanhMucSanPhamPartial()
        {
            var DanhMucSanPhamCha = db.LOAISANPHAMs.Where(n => n.MaLSPGoc == null).ToList();
            //.Select(n => new LOAISANPHAM { MaLSP = n.MaLSP, TenLSP = n.TenLSP, MaLSPGoc = n.MaLSPGoc, CC = db.LOAISANPHAMs.Where(x => x.MaLSPGoc == n.MaLSP) }).ToList()
            //var DanhMucSanPham1 = db.LOAISANPHAMs.ToList();
            //ViewBag.vBDanhLoaiSanPham = DanhMucSanPham;
            return PartialView(DanhMucSanPhamCha);
        }
       
        public PartialViewResult DanhMucSanPhamConPartial(int MaLSPCha)
        {
            var DanhMucSanPhamCon = db.LOAISANPHAMs.Where(n => n.MaLSPGoc == MaLSPCha).ToList();
            ViewBag.SoLuong = DanhMucSanPhamCon.Count();
            return PartialView(DanhMucSanPhamCon);
        }
        public ActionResult DanhMucSanPhamTheoNSXPartial(int? MaLSP)
        {
            //var DanhMucSanPhamTheoNSX = db.SANPHAMs.Where(n => n.MaLSP == 8).ToList();
            //var DanhMucSanPhamTheoNSX = db.SANPHAMs.Where(n => n.MaLSP == MaLSP).GroupBy(n => n.MaNSX).ToList();
            var sPCha = from sP in db.SANPHAMs join lSP in db.LOAISANPHAMs on sP.MaLSP equals lSP.MaLSP where sP.Xoa == false && lSP.MaLSPGoc == MaLSP select sP;
            ViewBag.MaLSP = MaLSP;
            var sPCha1 = sPCha.ToList();
            return PartialView(sPCha);
        }


   
    }
}