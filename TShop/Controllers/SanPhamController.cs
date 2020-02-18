﻿using System;
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
            var sp = db.LOAISANPHAMs.Where(n => n.MaLSP == MaLSP).ToList();
            //var sPCon = db.SANPHAMs.Where(n => n.MaLSP == MaLSP).ToList();
            var sPCha = from sP in db.SANPHAMs join lSP in db.LOAISANPHAMs on sP.MaLSP equals lSP.MaLSP where sP.Xoa == false && lSP.MaLSPGoc == MaLSP select sP;
            foreach (var item in sp)
            {
                if (item.MaLSPGoc == null)
                {
                    
                    sPCha = from sP in db.SANPHAMs join lSP in db.LOAISANPHAMs on sP.MaLSP equals lSP.MaLSP where sP.Xoa == false && lSP.MaLSPGoc == MaLSP select sP;
                }
                else
                {
                    sPCha = from sP in db.SANPHAMs join lSP in db.LOAISANPHAMs on sP.MaLSP equals lSP.MaLSP where sP.Xoa == false && lSP.MaLSP == MaLSP select sP;
                    //sPCha = db.SANPHAMs.Where(n => n.MaLSP == MaLSP && n.Xoa == false).ToList();
                }
            }
            //var sp1 = db.SANPHAMs.Where(n => n.MaLSP == MaLSP);
            //foreach(var item in sp1)
            //{
            //    item.
            //}
            //if(sp1)
            
            
            if (sPCha == null)
            {
                return HttpNotFound();
            }
            return View(sPCha);
        }
        public ActionResult SanPhamTheoLSPVaNSX(int? MaLSP,int? MaNSX)
        {
            if (MaLSP == null||MaNSX == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            var sP1 = from sP in db.SANPHAMs join lSP in db.LOAISANPHAMs on sP.MaLSP equals lSP.MaLSP where sP.Xoa == false && lSP.MaLSPGoc == MaLSP && sP.MaNSX==MaNSX select sP;
            
            //var sp = db.SANPHAMs.Where(n => n.MaLSP == MaLSP &&n.MaNSX == MaNSX && n.Xoa == false).ToList();


            //var Test = db.SANPHAMs.Where(n => n.MaLSP == MaLSP).ToList();
            //Test.GroupBy(n=>n.)
            if (sP1 == null)
            {
                return HttpNotFound();
            }
            return View(sP1);
        }
    }
}