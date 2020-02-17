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
        public ActionResult DanhMucSanPhamTheoNSXPartial(int MaLSP)
        {
            var DanhMucSanPhamTheoNSX = db.SANPHAMs.Where(n=>n.MaLSP==MaLSP).ToList();
           
            return PartialView(DanhMucSanPhamTheoNSX);
        }


   
    }
}