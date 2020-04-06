using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TShop.Models;
using PagedList;
using System.IO;
namespace TShop.Controllers
{
    public class AdminController : Controller
    {
        TShopEntities db = new TShopEntities();
        // GET: Admin
        //[ValidateInput(false)]
        //[HttpPost]
        //public ActionResult SuaSanPham(SANPHAM sp, HttpPostedFileBase HinhAnh, HttpPostedFileBase HinhAnh4, HttpPostedFileBase HinhAnh2, HttpPostedFileBase HinhAnh3)
        //{
        //    ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
        //    ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.MaLSP), "MaLSP", "TenLSP", sp.MaLSP);
        //    ViewBag.MaNSX = new SelectList(db.NHASANXUATs.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX", sp.MaNSX);
        //    var SP = db.SANPHAMs.SingleOrDefault(n => n.MaSP == sp.MaSP);

        //    if (HinhAnh != null)
        //    {
        //        var filename = Path.GetFileName(HinhAnh.FileName);
        //        var path = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename);
        //        if (System.IO.File.Exists(path))
        //        {
        //            ViewBag.Upload = "Hình ảnh đã tồn tại";

        //        }
        //        else
        //        {
        //            HinhAnh.SaveAs(path);
        //            sp.HinhAnh = filename;
        //            sp.HinhAnh1 = filename;
        //        }
        //    }
        //    else
        //    {
        //        sp.HinhAnh = SP.HinhAnh;
        //        sp.HinhAnh1 = SP.HinhAnh;
        //        //ViewBag.HinhAnh = "Thêm ít nhất 1 hình ảnh";

        //    }
        //    if (HinhAnh2 != null)
        //    {
        //        var filename2 = Path.GetFileName(HinhAnh2.FileName);
        //        var path2 = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename2);
        //        if (System.IO.File.Exists(path2))
        //        {
        //            ViewBag.Upload2 = "Hình ảnh đã tồn tại";
        //        }
        //        else
        //        {
        //            HinhAnh2.SaveAs(path2);
        //            sp.HinhAnh2 = filename2;
        //        }
        //    }
        //    else
        //    {
        //        sp.HinhAnh2 = SP.HinhAnh2;
        //    }
        //    if (HinhAnh3 != null)
        //    {
        //        var filename3 = Path.GetFileName(HinhAnh3.FileName);
        //        var path3 = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename3);
        //        if (System.IO.File.Exists(path3))
        //        {
        //            ViewBag.Upload3 = "Hình ảnh đã tồn tại";
        //        }
        //        else
        //        {
        //            HinhAnh3.SaveAs(path3);
        //            sp.HinhAnh3 = filename3;
        //        }
        //    }
        //    else
        //    {
        //        sp.HinhAnh3 = SP.HinhAnh3;
        //    }

        //    if (HinhAnh4 != null)
        //    {
        //        var filename4 = Path.GetFileName(HinhAnh4.FileName);
        //        var path4 = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename4);
        //        if (System.IO.File.Exists(path4))
        //        {
        //            ViewBag.Upload4 = "Hình ảnh đã tồn tại";
        //        }
        //        else
        //        {
        //            HinhAnh4.SaveAs(path4);
        //            sp.HinhAnh4 = filename4;
        //        }
        //    }
        //    else
        //    {
        //        sp.HinhAnh4 = SP.HinhAnh4;

        //    }
        //    sp.NgayCapNhat = DateTime.Now;
        //    sp.LuotMua = SP.LuotMua;
        //    sp.LuotXem = SP.LuotXem;
        //    sp.Xoa = SP.Xoa;

        //    db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("XemSanPham");

        //}
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NhapHang()
        {
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.ListSP = db.SANPHAMs;
            return View();
        }
        [HttpPost]
        public ActionResult NhapHang(PHIEUNHAP model, IEnumerable<CHITIETPHIEUNHAP> listModel)
        {
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.ListSP = db.SANPHAMs;

            model.Xoa = false;
            if (model.NgayNhap == null)
            {
                model.NgayNhap = DateTime.Now;
                db.PHIEUNHAPs.Add(model);
                db.SaveChanges();
            }
            else
            {
                var dt = Convert.ToDateTime(model.NgayNhap);
                DateTime dt1 = DateTime.Now;
                //dt.TimeOfDay.Add(dt1.TimeOfDay);
                //var gio = DateTime.Now.ToShortTimeString();
                model.NgayNhap = new DateTime(dt.Year, dt.Month, dt.Day, dt1.Hour, dt1.Minute, dt1.Second, dt1.Millisecond);
                db.PHIEUNHAPs.Add(model);
                db.SaveChanges();
            }

            SANPHAM sp;
            foreach (var item in listModel)
            {
                sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == item.MaSP);
                sp.SoLuongTon += item.SoLuongNhap;
                item.MaPN = model.MaPN;
            }
            db.CHITIETPHIEUNHAPs.AddRange(listModel);
            db.SaveChanges();
            ViewBag.ThongBao = "N";




            return View();
        }

        public ActionResult XemSanPham(int? Page)
        {
            // Số sản phẩm trên trang
            int PageSize = 10;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1

            var sp = db.SANPHAMs.Where(n => n.Xoa == false).ToList();
            return View(sp.OrderBy(n => n.MaSP).ToPagedList(PageNumber, PageSize));
        }
        [HttpGet]
        public ActionResult ThemSanPham()
        {

            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.MaLSP), "MaLSP", "TenLSP");
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX");

            //ViewBag.NCC = db.NHACUNGCAPs.ToList();
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ThemSanPham(SANPHAM sp, HttpPostedFileBase HinhAnh, HttpPostedFileBase HinhAnh2, HttpPostedFileBase HinhAnh3, HttpPostedFileBase HinhAnh4)
        {
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.MaLSP), "MaLSP", "TenLSP");
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX");
            ///
            SANPHAM SP = db.SANPHAMs.SingleOrDefault(n => n.TenSP == sp.TenSP);
            if (sp.TenSP == null)
            {
                ViewBag.TenSP = "Nhập tên sản phẩm";
                return View(sp);
            }
            if (SP != null)
            {
                ViewBag.TenSP = "Sản phẩm đã tồn tại";
                return View(sp);
            }
            if (HinhAnh != null)
            {
                var filename = Path.GetFileName(HinhAnh.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename);
                HinhAnh.SaveAs(path);
                sp.HinhAnh = filename;
                sp.HinhAnh1 = filename;

            }
            else
            {
                sp.HinhAnh = null;
                sp.HinhAnh1 = null;
                ViewBag.HinhAnh = "Thêm ít nhất 1 hình ảnh";
                return View(sp);
            }
            if (HinhAnh2 != null)
            {
                var filename = Path.GetFileName(HinhAnh2.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename);
                HinhAnh2.SaveAs(path);
                sp.HinhAnh2 = filename;
            }
            else
            {
                sp.HinhAnh2 = null;
            }
            if (HinhAnh3 != null)
            {
                var filename = Path.GetFileName(HinhAnh3.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename);
                HinhAnh3.SaveAs(path);
                sp.HinhAnh3 = filename;
            }
            else
            {
                sp.HinhAnh3 = null;
            }
            if (HinhAnh4 != null)
            {
                var filename = Path.GetFileName(HinhAnh4.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename);
                HinhAnh4.SaveAs(path);
                sp.HinhAnh4 = filename;
            }
            else
            {
                sp.HinhAnh4 = null;

            }
            sp.NgayCapNhat = DateTime.Now;
            sp.LuotMua = 0;
            sp.LuotXem = 0;
            sp.Xoa = false;
            db.SANPHAMs.Add(sp);
            db.SaveChanges();
            return RedirectToAction("XemSanPham");
            //if (HinhAnh != null)
            //{
            //    var filename = Path.GetFileName(HinhAnh.FileName);
            //    var path = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename);
            //    if (System.IO.File.Exists(path))
            //    {
            //        ViewBag.Upload = "Hình ảnh đã tồn tại";

            //        return View(sp);
            //    }
            //    else
            //    {
            //        HinhAnh.SaveAs(path);
            //        sp.HinhAnh = filename;
            //        sp.HinhAnh1 = filename;
            //    }
            //}
            //else
            //{
            //    sp.HinhAnh = null;
            //    sp.HinhAnh1 = null;
            //    ViewBag.HinhAnh = "Thêm ít nhất 1 hình ảnh";
            //    return View(sp);
            //}
            //if (HinhAnh2 != null)
            //{
            //    var filename2 = Path.GetFileName(HinhAnh2.FileName);
            //    var path2 = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename2);
            //    if (System.IO.File.Exists(path2))
            //    {
            //        var pdl = Path.Combine(Server.MapPath("~/Content/HinhAnh"), sp.HinhAnh);
            //        System.IO.File.Delete(pdl);
            //        ViewBag.Upload2 = "Hình ảnh đã tồn tại";
            //        return View(sp);
            //    }
            //    else
            //    {
            //        HinhAnh2.SaveAs(path2);
            //        sp.HinhAnh2 = filename2;
            //    }
            //}
            //else
            //{
            //    sp.HinhAnh2 = null;
            //}
            //if (HinhAnh3 != null)
            //{
            //    var filename3 = Path.GetFileName(HinhAnh3.FileName);
            //    var path3 = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename3);
            //    if (System.IO.File.Exists(path3))
            //    {
            //        var pdl = Path.Combine(Server.MapPath("~/Content/HinhAnh"), sp.HinhAnh);
            //        System.IO.File.Delete(pdl);
            //        var pdl2 = Path.Combine(Server.MapPath("~/Content/HinhAnh"), sp.HinhAnh2);
            //        System.IO.File.Delete(pdl2);
            //        ViewBag.Upload3 = "Hình ảnh đã tồn tại";
            //        return View(sp);
            //    }
            //    else
            //    {
            //        HinhAnh3.SaveAs(path3);
            //        sp.HinhAnh3 = filename3;
            //    }
            //}
            //else
            //{
            //    sp.HinhAnh3 = null;
            //}

            //if (HinhAnh4 != null)
            //{
            //    var filename4 = Path.GetFileName(HinhAnh4.FileName);
            //    var path4 = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename4);
            //    if (System.IO.File.Exists(path4))
            //    {
            //        var pdl = Path.Combine(Server.MapPath("~/Content/HinhAnh"), sp.HinhAnh);
            //        System.IO.File.Delete(pdl);
            //        var pdl2 = Path.Combine(Server.MapPath("~/Content/HinhAnh"), sp.HinhAnh2);
            //        System.IO.File.Delete(pdl2);
            //        var pdl3 = Path.Combine(Server.MapPath("~/Content/HinhAnh"), sp.HinhAnh3);
            //        System.IO.File.Delete(pdl3);
            //        ViewBag.Upload4 = "Hình ảnh đã tồn tại";
            //        return View(sp);
            //    }
            //    else
            //    {
            //        HinhAnh4.SaveAs(path4);
            //        sp.HinhAnh4 = filename4;
            //    }
            //}
            //else
            //{
            //    sp.HinhAnh4 = null;

            //}

            //if (SP != null)
            //{
            //    ViewBag.TenSP = "Sản phẩm đã tồn tại";
            //}
            //else
            //{
            //    sp.NgayCapNhat = DateTime.Now;
            //    sp.LuotMua = 0;
            //    sp.LuotXem = 0;
            //    sp.Xoa = false;
            //    db.SANPHAMs.Add(sp);
            //    db.SaveChanges();
            //    return RedirectToAction("XemSanPham");
            //}

            //ViewBag.ABC = "Thêm sản phẩm không thành công";
            //return View();
        }

        public ActionResult SuaSanPham(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.MaLSP), "MaLSP", "TenLSP", sp.MaLSP);
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX", sp.MaNSX);


            return View(sp);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SuaSanPham(SANPHAM sp, HttpPostedFileBase HinhAnh, HttpPostedFileBase HinhAnh4, HttpPostedFileBase HinhAnh2, HttpPostedFileBase HinhAnh3)
        {
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.MaLSP), "MaLSP", "TenLSP", sp.MaLSP);
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX", sp.MaNSX);
            //var SP = db.SANPHAMs.SingleOrDefault(n => n.MaSP == sp.MaSP);
            if (sp.TenSP == null)
            {
                ViewBag.TenSP = "Nhập tên sản phẩm";
                return View(sp);
            }
            //if (sp.TenSP != SP.TenSP && SP != null)
            //{
            //    ViewBag.TenSP = "Sản phẩm đã tồn tại";
            //    return View(sp);
            //}
            if (HinhAnh != null)
            {
                var filename = Path.GetFileName(HinhAnh.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename);
                HinhAnh.SaveAs(path);
                sp.HinhAnh = filename;
                sp.HinhAnh1 = filename;

            }

            if (HinhAnh2 != null)
            {
                var filename = Path.GetFileName(HinhAnh2.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename);
                HinhAnh2.SaveAs(path);
                sp.HinhAnh2 = filename;
            }

            if (HinhAnh3 != null)
            {
                var filename = Path.GetFileName(HinhAnh3.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename);
                HinhAnh3.SaveAs(path);
                sp.HinhAnh3 = filename;
            }

            if (HinhAnh4 != null)
            {
                var filename = Path.GetFileName(HinhAnh4.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename);
                HinhAnh4.SaveAs(path);
                sp.HinhAnh4 = filename;
            }

            //if (HinhAnh != null)
            //{
            //    var filename = Path.GetFileName(HinhAnh.FileName);
            //    var path = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename);
            //    if (System.IO.File.Exists(path))
            //    {
            //        ViewBag.Upload = "Hình ảnh đã tồn tại";
            //        return View(sp);
            //    }
            //    else
            //    {
            //        if (sp.HinhAnh != null)
            //        {
            //            var pdl = Path.Combine(Server.MapPath("~/Content/HinhAnh"), sp.HinhAnh);
            //            System.IO.File.Delete(pdl);

            //        }
            //        HinhAnh.SaveAs(path);
            //        sp.HinhAnh = filename;
            //        sp.HinhAnh1 = filename;

            //    }
            //}
            //else
            //{
            //    //sp.HinhAnh = SP.HinhAnh;
            //    //sp.HinhAnh1 = SP.HinhAnh;
            //    //ViewBag.HinhAnh = "Thêm ít nhất 1 hình ảnh";

            //}
            //if (HinhAnh2 != null)
            //{
            //    var filename2 = Path.GetFileName(HinhAnh2.FileName);
            //    var path2 = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename2);
            //    if (System.IO.File.Exists(path2))
            //    {
            //        ViewBag.Upload2 = "Hình ảnh đã tồn tại";
            //        return View(sp);
            //    }
            //    else
            //    {
            //        if (sp.HinhAnh2 != null)
            //        {
            //            var p2dl = Path.Combine(Server.MapPath("~/Content/HinhAnh"), sp.HinhAnh2);
            //            System.IO.File.Delete(p2dl);
            //        }

            //        HinhAnh2.SaveAs(path2);
            //        sp.HinhAnh2 = filename2;
            //    }
            //}
            //else
            //{
            //    //sp.HinhAnh2 = SP.HinhAnh2;
            //}
            //if (HinhAnh3 != null)
            //{
            //    var filename3 = Path.GetFileName(HinhAnh3.FileName);
            //    var path3 = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename3);
            //    if (System.IO.File.Exists(path3))
            //    {
            //        ViewBag.Upload3 = "Hình ảnh đã tồn tại";
            //        return View(sp);
            //    }
            //    else
            //    {
            //        if (sp.HinhAnh3 != null)
            //        {
            //            var p3dl = Path.Combine(Server.MapPath("~/Content/HinhAnh"), sp.HinhAnh3);
            //            System.IO.File.Delete(p3dl);
            //        }

            //        HinhAnh3.SaveAs(path3);
            //        sp.HinhAnh3 = filename3;
            //    }
            //}
            //else
            //{
            //    //sp.HinhAnh3 = SP.HinhAnh3;
            //}

            //if (HinhAnh4 != null)
            //{
            //    var filename4 = Path.GetFileName(HinhAnh4.FileName);
            //    var path4 = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename4);
            //    if (System.IO.File.Exists(path4))
            //    {
            //        ViewBag.Upload4 = "Hình ảnh đã tồn tại";
            //        return View(sp);
            //    }
            //    else
            //    {
            //        if (sp.HinhAnh4 != null)
            //        {
            //            var p4dl = Path.Combine(Server.MapPath("~/Content/HinhAnh"), sp.HinhAnh4);
            //            System.IO.File.Delete(p4dl);
            //        }

            //        HinhAnh4.SaveAs(path4);
            //        sp.HinhAnh4 = filename4;


            //    }
            //}
            //else
            //{
            //    //sp.HinhAnh4 = SP.HinhAnh4;

            //}

            db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("XemSanPham");
        }
        public ActionResult XoaSanPham(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.MaLSP), "MaLSP", "TenLSP", sp.MaLSP);
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX", sp.MaNSX);


            return View(sp);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult XoaSanPham(SANPHAM sp, HttpPostedFileBase HinhAnh, HttpPostedFileBase HinhAnh4, HttpPostedFileBase HinhAnh2, HttpPostedFileBase HinhAnh3)
        {
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.MaLSP), "MaLSP", "TenLSP", sp.MaLSP);
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX", sp.MaNSX);
            //var SP = db.SANPHAMs.SingleOrDefault(n => n.MaSP == sp.MaSP);
            if (sp.TenSP == null)
            {
                ViewBag.TenSP = "Nhập tên sản phẩm";
                return View(sp);
            }
            //if (sp.TenSP != SP.TenSP && SP != null)
            //{
            //    ViewBag.TenSP = "Sản phẩm đã tồn tại";
            //    return View(sp);
            //}
            if (HinhAnh != null)
            {
                var filename = Path.GetFileName(HinhAnh.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename);
                HinhAnh.SaveAs(path);
                sp.HinhAnh = filename;
                sp.HinhAnh1 = filename;

            }

            if (HinhAnh2 != null)
            {
                var filename = Path.GetFileName(HinhAnh2.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename);
                HinhAnh2.SaveAs(path);
                sp.HinhAnh2 = filename;
            }

            if (HinhAnh3 != null)
            {
                var filename = Path.GetFileName(HinhAnh3.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename);
                HinhAnh3.SaveAs(path);
                sp.HinhAnh3 = filename;
            }

            if (HinhAnh4 != null)
            {
                var filename = Path.GetFileName(HinhAnh4.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename);
                HinhAnh4.SaveAs(path);
                sp.HinhAnh4 = filename;
            }

            sp.Xoa = true;
            db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("XemSanPham");
        }
    
        public ActionResult XemNhaCungCap(int? Page)
        {
            // Số sản phẩm trên trang
            int PageSize = 10;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1

            var ncc = db.NHACUNGCAPs.Where(n => n.Xoa == false).ToList();
            return View(ncc.OrderBy(n => n.MaNCC).ToPagedList(PageNumber, PageSize));
        }
        [HttpGet]
        public ActionResult ThemNhaCungCap()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ThemNhaCungCap(NHACUNGCAP ncc)
        {
            NHACUNGCAP NCC = db.NHACUNGCAPs.SingleOrDefault(n => n.TenNCC == ncc.TenNCC);

            if (ncc.TenNCC == null)
            {
                ViewBag.ThongBao = "Nhập tên nhà cung cấp";
                return View(NCC);
            }
            if (NCC != null)
            {
                ViewBag.ThongBao = "Nhà cung cấp đã tồn tại";
                return View(NCC);
            }

            db.NHACUNGCAPs.Add(ncc);
            db.SaveChanges();
            ViewBag.ThongBao = "Thêm nhà cung cấp thành công";

            return View();

        }
        public ActionResult SuaNhaCungCap(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            NHACUNGCAP ncc = db.NHACUNGCAPs.SingleOrDefault(n => n.MaNCC == id);
            if (ncc == null)
            {
                return HttpNotFound();
            }
            //ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            //ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.MaLSP), "MaLSP", "TenLSP", sp.MaLSP);
            //ViewBag.MaNSX = new SelectList(db.NHASANXUATs.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX", sp.MaNSX);


            return View(ncc);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SuaNhaCungCap(NHACUNGCAP ncc)
        {
            //ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            //ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.MaLSP), "MaLSP", "TenLSP", sp.MaLSP);
            //ViewBag.MaNSX = new SelectList(db.NHASANXUATs.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX", sp.MaNSX);
            //var SP = db.SANPHAMs.SingleOrDefault(n => n.MaSP == sp.MaSP);
            if (ncc.TenNCC == null)
            {
                ViewBag.NCC = "Nhập tên sản phẩm";
                return View(ncc);
            }
            db.Entry(ncc).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("XemNhaCungCap");
        }
        public ActionResult XoaNhaCungCap(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            NHACUNGCAP ncc = db.NHACUNGCAPs.SingleOrDefault(n => n.MaNCC == id);
            if (ncc == null)
            {
                return HttpNotFound();
            }
            return View(ncc);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult XoaNhaCungCap(NHACUNGCAP ncc)
        {
        
            if (ncc.TenNCC == null)
            {
                ViewBag.TenNCC = "Nhập tên nhà cung cấp";
                return View(ncc);
            }
 
            ncc.Xoa = true;
            db.Entry(ncc).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("XemNhaCungCap");
        }


        public ActionResult XemNhaSanXuat(int? Page)
        {
            // Số sản phẩm trên trang
            int PageSize = 10;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1

            var sp = db.NHASANXUATs.Where(n => n.Xoa == false).ToList();
            return View(sp.OrderBy(n => n.MaNSX).ToPagedList(PageNumber, PageSize));
        }
        [HttpGet]
        public ActionResult ThemNhaSanXuat()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ThemNhaSanXuat(NHASANXUAT nsx, HttpPostedFileBase HinhAnh)
        {
            NHASANXUAT NSX = db.NHASANXUATs.SingleOrDefault(n => n.TenNSX == nsx.TenNSX);

            if (nsx.TenNSX == null)
            {
                ViewBag.ThongBao = "Nhập tên nhà sản xuất";
                return View(NSX);
            }
            if (NSX != null)
            {
                ViewBag.ThongBao = "Nhà sản xuất đã tồn tại";
                return View(NSX);
            }
            if (HinhAnh != null)
            {
                var filename = Path.GetFileName(HinhAnh.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnh/ThuongHieu/"), filename);
                HinhAnh.SaveAs(path);
                nsx.HinhAnh = filename;

            }
            else
            {
                nsx.HinhAnh = null;

                //ViewBag.HinhAnh = "Thêm ít nhất 1 hình ảnh";
                //return View(nsx);
            }

            db.NHASANXUATs.Add(nsx);
            db.SaveChanges();
            ViewBag.ThongBao = "Thêm nhà sản xuất thành công";

            return View();

        }
        public ActionResult SuaNhaSanXuat(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            NHASANXUAT nsx = db.NHASANXUATs.SingleOrDefault(n => n.MaNSX == id);
            if (nsx == null)
            {
                return HttpNotFound();
            }
     
            return View(nsx);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SuaNhaSanXuat(NHASANXUAT nsx, HttpPostedFileBase HinhAnh)
        {
            //ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            //ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.MaLSP), "MaLSP", "TenLSP", sp.MaLSP);
            //ViewBag.MaNSX = new SelectList(db.NHASANXUATs.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX", sp.MaNSX);
            //var SP = db.SANPHAMs.SingleOrDefault(n => n.MaSP == sp.MaSP);
            if (nsx.TenNSX == null)
            {
                ViewBag.TenNSX = "Nhập tên nhà sản xuất";
                return View(nsx);
            }
            //if (sp.TenSP != SP.TenSP && SP != null)
            //{
            //    ViewBag.TenSP = "Sản phẩm đã tồn tại";
            //    return View(sp);
            //}
            if (HinhAnh != null)
            {
                var filename = Path.GetFileName(HinhAnh.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnh/ThuongHieu"), filename);
                HinhAnh.SaveAs(path);
                nsx.HinhAnh = filename;
            }


            db.Entry(nsx).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("XemNhaSanXuat");
        }
        public ActionResult XoaNhaSanXuat(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            NHASANXUAT nsx = db.NHASANXUATs.SingleOrDefault(n => n.MaNSX == id);
            if (nsx == null)
            {
                return HttpNotFound();
            }
       


            return View(nsx);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult XoaNhaSanXuat(NHASANXUAT nsx, HttpPostedFileBase HinhAnh)
        {

            //var SP = db.SANPHAMs.SingleOrDefault(n => n.MaSP == sp.MaSP);
            if (nsx.TenNSX == null)
            {
                ViewBag.TenNSX = "Nhập tên sản phẩm";
                return View(nsx);
            }
            //if (sp.TenSP != SP.TenSP && SP != null)
            //{
            //    ViewBag.TenSP = "Sản phẩm đã tồn tại";
            //    return View(sp);
            //}
            if (HinhAnh != null)
            {
                var filename = Path.GetFileName(HinhAnh.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnh/ThuongHieu/"), filename);
                HinhAnh.SaveAs(path);
                nsx.HinhAnh = filename;

            }
            nsx.Xoa = true;
            db.Entry(nsx).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("XemNhaSanXuat");
        }
        public ActionResult XemLoaiSanPham(int? Page)
        {
            // Số sản phẩm trên trang
            int PageSize = 10;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1

            var lsp = db.LOAISANPHAMs.Where(n => n.Xoa == false).ToList();
            return View(lsp.OrderBy(n => n.MaLSP).ToPagedList(PageNumber, PageSize));
        }
        [HttpGet]
        public ActionResult ThemLoaiSanPham()
        {
            ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.MaLSP), "MaLSPGoc", "TenLSP");
            return View();
        }
        [HttpPost]
        public ActionResult ThemLoaiSanPham(LOAISANPHAM lsp, HttpPostedFileBase HinhAnh)
        {
            LOAISANPHAM LSP = db.LOAISANPHAMs.SingleOrDefault(n => n.TenLSP == lsp.TenLSP);

            if (lsp.TenLSP == null)
            {
                ViewBag.ThongBao = "Nhập tên loại sản phẩm";
                return View(LSP);
            }
            if (LSP != null)
            {
                ViewBag.ThongBao = "Loai sản phẩm đã tồn tại";
                return View(LSP);
            }
            if (HinhAnh != null)
            {
                var filename = Path.GetFileName(HinhAnh.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnh/Icon/"), filename);
                HinhAnh.SaveAs(path);
                lsp.HinhAnh = filename;

            }
            else
            {
                lsp.HinhAnh = null;

                //ViewBag.HinhAnh = "Thêm ít nhất 1 hình ảnh";
                //return View(nsx);
            }

            db.LOAISANPHAMs.Add(lsp);
            db.SaveChanges();
            ViewBag.ThongBao = "Thêm loại sản phẩm thành công";

            return View();

        }
        public ActionResult SuaLoaiSanPham(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            LOAISANPHAM lsp = db.LOAISANPHAMs.SingleOrDefault(n => n.MaLSP == id);
            if (lsp == null)
            {
                return HttpNotFound();
            }
            return View(lsp);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SuaLoaiSanPham(LOAISANPHAM lsp, HttpPostedFileBase HinhAnh)
        {
        
            //var SP = db.SANPHAMs.SingleOrDefault(n => n.MaSP == sp.MaSP);
            if (lsp.TenLSP == null)
            {
                ViewBag.TenLSP = "Nhập tên loại sản phẩm";
                return View(lsp);
            }
            //if (sp.TenSP != SP.TenSP && SP != null)
            //{
            //    ViewBag.TenSP = "Sản phẩm đã tồn tại";
            //    return View(sp);
            //}
            if (HinhAnh != null)
            {
                var filename = Path.GetFileName(HinhAnh.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Icon"), filename);
                HinhAnh.SaveAs(path);
                lsp.HinhAnh = filename;
            

            }

            db.Entry(lsp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("XemloaiSanPham");
        }
        public ActionResult XoaLoaiSanPham(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            LOAISANPHAM lsp = db.LOAISANPHAMs.SingleOrDefault(n => n.MaLSP == id);
            if (lsp == null)
            {
                return HttpNotFound();
            }
    
            return View(lsp);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult XoaLoaiSanPham(LOAISANPHAM lsp, HttpPostedFileBase HinhAnh)
        {

            //var SP = db.SANPHAMs.SingleOrDefault(n => n.MaSP == sp.MaSP);
            if (lsp.TenLSP == null)
            {
                ViewBag.TenLSP = "Nhập tên loại sản phẩm";
                return View(lsp);
            }
   
            if (HinhAnh != null)
            {
                var filename = Path.GetFileName(HinhAnh.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Icon"), filename);
                HinhAnh.SaveAs(path);
                lsp.HinhAnh = filename;
               

            }
            lsp.Xoa = true;
            db.Entry(lsp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("XemloaiSanPham");
        }


        public ActionResult KetQuaTimKiem(string TuKhoa, int? Page)
        {

            var listSanPham = db.SANPHAMs.Where(n => n.TenSP.Contains(TuKhoa) && n.Xoa == false);
            // Chức năng phân trang
            // Số sản phẩm trên trang
            int PageSize = 10;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1
            ViewBag.TuKhoa = TuKhoa;
            return View(listSanPham.OrderBy(n => n.TenSP).ToPagedList(PageNumber, PageSize));
        }

        //public ActionResult TimKiem(string TuKhoa)
        //{
        //    var sp = db.SANPHAMs.Where(p => p.TenSP.Contains(TuKhoa)).Select(p => p.TenSP).ToList();
        //    return Json(sp, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult Search(string term)
        {
            var names = db.SANPHAMs.Where(p => p.TenSP.Contains(term)).Select(p => p.TenSP).ToList();
            return Json(names, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getemp(string ename)
        {
            var emp = db.SANPHAMs.Where(p => p.TenSP.Contains(ename) && p.Xoa == false).Select(p => p.MaSP + " - " + p.TenSP).Take(10).ToList();
            return Json(emp);
        }


        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            return View();
        }

        public ActionResult ChoXacNhan(int? Page)
        {
            // Số sản phẩm trên trang
            int PageSize = 10;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1

            var cxn = db.DONDATHANGs.Where(n => n.TinhTrangGiaoHang == "cxn" && n.Xoa==false).OrderBy(n => n.NgayDat);
            return View(cxn.OrderBy(n => n.MaDDH).ToPagedList(PageNumber, PageSize));
        }
        public ActionResult ChuaGiaoDaThanhToan(int? Page)
        {
            // Số sản phẩm trên trang
            int PageSize = 10;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1

            var donChuaGiao = db.DONDATHANGs.Where(n => n.TinhTrangGiaoHang == "cgdtt" && n.Xoa == false).OrderBy(n => n.NgayDat);
            return View(donChuaGiao.OrderBy(n => n.MaDDH).ToPagedList(PageNumber, PageSize));
        }
        public ActionResult ChuaGiaoTest(int? Page)
        {
            // Số sản phẩm trên trang
            int PageSize = 10;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1

            var donChuaGiao = db.DONDATHANGs.Where(n => n.TinhTrangGiaoHang == "Chưa giao" && n.Xoa == false).OrderBy(n => n.NgayDat);
            return View(donChuaGiao.OrderBy(n => n.MaDDH).ToPagedList(PageNumber, PageSize));
        }
    }
}