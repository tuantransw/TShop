using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TShop.Models;
namespace TShop.Controllers
{
    [Authorize]
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

        //[Authorize(Roles="1")]
        public ActionResult Index()
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();
            ViewBag.SoNguoiOnline = HttpContext.Application["Online"].ToString();
            ViewBag.ThongKeTongDoanhThu = ThongKeTongDoanhThu();
            //ViewBag.ThongKeDoanhThuThang = ThongKeDoanhThuThang();
            ViewBag.ThongKeDonHang = ThongKeDonHang();
            ViewBag.ThongKeKhachHang = ThongKeKhachHang();
            return View();
        }
        [Authorize(Roles = "5")]
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
                if (sp.SoLuongTon == null)
                {
                    sp.SoLuongTon = item.SoLuongNhap;
                }
                else
                {
                    sp.SoLuongTon += item.SoLuongNhap;
                }
              
                item.MaPN = model.MaPN;
            }
            db.CHITIETPHIEUNHAPs.AddRange(listModel);
            db.SaveChanges();
            ViewBag.ThongBao = "N";




            return View();
        }

        [Authorize(Roles = "2")]
        public ActionResult XemSanPham(int? Page)
        {
            // Số sản phẩm trên trang
            int PageSize = 10;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1

            var sp = db.SANPHAMs.Where(n => n.Xoa == false).ToList();
            return View(sp.OrderBy(n => n.MaSP).ToPagedList(PageNumber, PageSize));
        }
        [Authorize(Roles = "2")]
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

        [Authorize(Roles = "2")]
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
        [Authorize(Roles = "2")]
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

        [Authorize(Roles = "2")]
        public ActionResult XemNhaCungCap(int? Page)
        {
            // Số sản phẩm trên trang
            int PageSize = 10;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1

            var ncc = db.NHACUNGCAPs.Where(n => n.Xoa == false).ToList();
            return View(ncc.OrderBy(n => n.MaNCC).ToPagedList(PageNumber, PageSize));
        }
        [Authorize(Roles = "2")]
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
        [Authorize(Roles = "2")]
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
        [Authorize(Roles = "2")]
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

        [Authorize(Roles = "5")]
        public ActionResult XemNhaSanXuat(int? Page)
        {
            // Số sản phẩm trên trang
            int PageSize = 10;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1

            var sp = db.NHASANXUATs.Where(n => n.Xoa == false).ToList();
            return View(sp.OrderBy(n => n.MaNSX).ToPagedList(PageNumber, PageSize));
        }
        [Authorize(Roles = "5")]
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
        [Authorize(Roles = "5")]
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
        [Authorize(Roles = "5")]
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
        [Authorize(Roles = "4")]
        public ActionResult XemLoaiSanPham(int? Page)
        {
            // Số sản phẩm trên trang
            int PageSize = 10;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1

            var lsp = db.LOAISANPHAMs.Where(n => n.Xoa == false).ToList();
            return View(lsp.OrderBy(n => n.MaLSP).ToPagedList(PageNumber, PageSize));
        }
        [Authorize(Roles = "4")]
        [HttpGet]
        public ActionResult ThemLoaiSanPham()
        {
            //ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.MaLSP), "MaLSPGoc", "TenLSP");
            //ViewBag.MaLSPG = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.MaLSP), "MaLSPGoc", "TenLSP");
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
        [Authorize(Roles = "4")]
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
        [Authorize(Roles = "4")]
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
        public ActionResult TatCaPhieuNhapHang(int? Page)
        {
            // Số sản phẩm trên trang
            int PageSize = 10;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1

            var pn = db.PHIEUNHAPs.Where(n => n.Xoa == false);
            return View(pn.OrderByDescending(n => n.NgayNhap).ToPagedList(PageNumber, PageSize));
        }
        public ActionResult TatCaPhieuNhapHangDaXoa(int? Page)
        {
            // Số sản phẩm trên trang
            int PageSize = 10;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1

            var pn = db.PHIEUNHAPs.Where(n => n.Xoa == true);
            return View(pn.OrderByDescending(n => n.NgayNhap).ToPagedList(PageNumber, PageSize));
        }
        [Authorize(Roles = "7")]
        public ActionResult ChiTietPhieuNhapHang(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            PHIEUNHAP pn = db.PHIEUNHAPs.SingleOrDefault(n => n.MaPN == id);
            if (pn == null)
            {
                return HttpNotFound();
            }
            var listCTPNH = db.CHITIETPHIEUNHAPs.Where(n => n.MaPN == id);
            ViewBag.listCTPNH = listCTPNH;
            return View(pn);
        }
       
        public ActionResult XoaPhieuNhapHang(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            PHIEUNHAP pn = db.PHIEUNHAPs.SingleOrDefault(n => n.MaPN == id);
            if (pn == null)
            {
                return HttpNotFound();
            }
            pn.Xoa = true;
            //ddh.HuyDatHang = true;
            db.SaveChanges();
            //var listCTDDH = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == id);
            //ViewBag.listCTDDH = listCTDDH;
            return RedirectToAction("TatCaPhieuNhapHang", "Admin");
        }
        [Authorize(Roles = "7")]
        public ActionResult ChoXacNhan(int? Page)
        {
            // Số sản phẩm trên trang
            int PageSize = 10;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1

            var cxn = db.DONDATHANGs.Where(n => n.TinhTrangGiaoHang == "cxn" && n.Xoa == false && n.HuyDatHang == false);
            return View(cxn.OrderBy(n => n.NgayDat).ToPagedList(PageNumber, PageSize));
        }
        [Authorize(Roles = "7")]
        public ActionResult DangGiao(int? Page)
        {
            // Số sản phẩm trên trang
            int PageSize = 10;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1

            var cxn = db.DONDATHANGs.Where(n => n.TinhTrangGiaoHang == "dxn" && n.Xoa == false && n.HuyDatHang == false);
            return View(cxn.OrderBy(n => n.NgayDat).ToPagedList(PageNumber, PageSize));
        }
        [Authorize(Roles = "7")]
        public ActionResult DaHoanThanh(int? Page)
        {
            // Số sản phẩm trên trang
            int PageSize = 10;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1

            var cxn = db.DONDATHANGs.Where(n => n.TinhTrangGiaoHang == "ht" && n.Xoa == false && n.HuyDatHang == false);
            return View(cxn.OrderByDescending(n => n.NgayGiao).ToPagedList(PageNumber, PageSize));
        }
        [Authorize(Roles = "7")]
        public ActionResult DonDaHuy(int? Page)
        {
            // Số sản phẩm trên trang
            int PageSize = 10;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1

            var cxn = db.DONDATHANGs.Where(n => n.HuyDatHang == true && n.Xoa == false);
            return View(cxn.OrderByDescending(n => n.NgayDat).ToPagedList(PageNumber, PageSize));
        }
        [Authorize(Roles = "7")]
        public ActionResult XemDonHang(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            DONDATHANG ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDDH == id);
            if (ddh == null)
            {
                return HttpNotFound();
            }
            var listCTDDH = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == id);
            ViewBag.listCTDDH = listCTDDH;
            return View(ddh);
        }
        [Authorize(Roles = "7")]
        public ActionResult DuyetDonHang(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            DONDATHANG ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDDH == id);
            if (ddh == null)
            {
                return HttpNotFound();
            }
            var listCTDDH = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == id);
            ViewBag.listCTDDH = listCTDDH;
            return View(ddh);
        }
        [HttpPost]
        public ActionResult DuyetDonHang(DONDATHANG ddh)
        {

            DONDATHANG luuddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDDH == ddh.MaDDH);

            luuddh.TinhTrangGiaoHang = "dxn";
            if (ddh.DaThanhToan == null)
            {
                luuddh.DaThanhToan = false;
            }
            else
            { luuddh.DaThanhToan = ddh.DaThanhToan; }
            var listSPCTDDH = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == ddh.MaDDH);
            foreach (var item in listSPCTDDH)
            {
                SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == item.MaSP);
                if (sp == null || sp.Xoa == true)
                {
                    ViewBag.ThongBao = "Sản phẩm không tồn tại hoặc đã bị xoá";
                    var listCTDDH1 = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == ddh.MaDDH);
                    ViewBag.listCTDDH = listCTDDH1;
                    return View(luuddh);
                }
                if (sp.SoLuongTon <= 0 || sp.SoLuongTon == null || sp.SoLuongTon < item.SoLuong)
                {
                    var listCTDDH1 = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == ddh.MaDDH);
                    ViewBag.listCTDDH = listCTDDH1;
                    ViewBag.ThongBao = "Sản phẩm đã hết hàng";
                    return View(luuddh);
                }
                else
                {
                    sp.SoLuongTon = int.Parse(sp.SoLuongTon.ToString()) - int.Parse(item.SoLuong.ToString());
                    sp.LuotMua = int.Parse(sp.LuotMua.ToString()) + 1;

                }
                //db.SaveChanges();
            }
            db.SaveChanges();
            var listCTDDH = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == ddh.MaDDH);
            ViewBag.listCTDDH = listCTDDH;
            GuiEmail("Xác nhận mua hàng Tshop", luuddh.KHACHHANG.Email, "tshop.tuantran.sw@gmail.com", "abc@123Abc", "Đơn mua của bạn đã được xác nhận thành công. Đơn hàng sẽ được gửi đến trong vài ngày.");
            ViewBag.ThongBao = "Lưu đơn hàng thành công.";
            ViewBag.ThongBao1 = "Gửi email thông báo cho khách hàng thành công.";
            return View(luuddh);
        }
        public ActionResult DuyetDonHangDangGiao(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            DONDATHANG ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDDH == id);
            if (ddh == null)
            {
                return HttpNotFound();
            }
            var listCTDDH = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == id);
            ViewBag.listCTDDH = listCTDDH;
            return View(ddh);
        }
        public ActionResult HuyDonHangChoXacNhan(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            DONDATHANG ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDDH == id);
            if (ddh == null)
            {
                return HttpNotFound();
            }
            if (ddh.TinhTrangGiaoHang == "cxn")
            {
                ddh.TinhTrangGiaoHang = "hd";
                ddh.HuyDatHang = true;
            }
            else
            {
                ddh.TinhTrangGiaoHang = "hd";
                ddh.HuyDatHang = true;
                var listSPCTDDH = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == ddh.MaDDH);
                foreach (var item in listSPCTDDH)
                {
                    SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == item.MaSP);
                    if (sp == null || sp.Xoa == true)
                    {
                        ViewBag.ThongBao = "Sản phẩm không tồn tại hoặc đã bị xoá";
                        var listCTDDH1 = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == ddh.MaDDH);
                        ViewBag.listCTDDH = listCTDDH1;
                        return RedirectToAction("DuyetDonHang", new { @id = id });
                    }
                    if (sp.SoLuongTon <= 0 || sp.SoLuongTon == null)
                    {
                        var listCTDDH1 = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == ddh.MaDDH);
                        ViewBag.listCTDDH = listCTDDH1;
                        ViewBag.ThongBao = "Sản phẩm đã hết hàng";
                        return RedirectToAction("DuyetDonHang", new { @id = id });
                    }
                    else
                    {
                        sp.SoLuongTon = int.Parse(sp.SoLuongTon.ToString()) + int.Parse(item.SoLuong.ToString());
                        sp.LuotMua = int.Parse(sp.LuotMua.ToString()) - 1;

                    }
                }

            }
            //ddh.TinhTrangGiaoHang = "hd";
            //ddh.HuyDatHang = true;
            db.SaveChanges();
            //var listCTDDH = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == id);
            //ViewBag.listCTDDH = listCTDDH;
            return RedirectToAction("ChoXacNhan", "Admin");
        }
        public ActionResult XoaDonHang(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            DONDATHANG ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDDH == id);
            if (ddh == null)
            {
                return HttpNotFound();
            }
            ddh.Xoa = true;
            //ddh.HuyDatHang = true;
            db.SaveChanges();
            //var listCTDDH = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == id);
            //ViewBag.listCTDDH = listCTDDH;
            return RedirectToAction("DaHoanThanh", "Admin");
        }
        public ActionResult HuyDonHangDangGiao(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            DONDATHANG ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDDH == id);
            if (ddh == null)
            {
                return HttpNotFound();
            }
            if (ddh.TinhTrangGiaoHang == "cxn")
            {
                ddh.TinhTrangGiaoHang = "hd";
                ddh.HuyDatHang = true;
            }
            else
            {
                ddh.TinhTrangGiaoHang = "hd";
                ddh.HuyDatHang = true;
                var listSPCTDDH = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == ddh.MaDDH);
                foreach (var item in listSPCTDDH)
                {
                    SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == item.MaSP);
                    if (sp == null || sp.Xoa == true)
                    {
                        ViewBag.ThongBao = "Sản phẩm không tồn tại hoặc đã bị xoá";
                        var listCTDDH1 = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == ddh.MaDDH);
                        ViewBag.listCTDDH = listCTDDH1;
                        return RedirectToAction("DuyetDonHangDangGiao", new { @id = id });
                    }
                    if (sp.SoLuongTon <= 0 || sp.SoLuongTon == null)
                    {
                        var listCTDDH1 = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == ddh.MaDDH);
                        ViewBag.listCTDDH = listCTDDH1;
                        ViewBag.ThongBao = "Sản phẩm đã hết hàng";
                        return RedirectToAction("DuyetDonHangDangGiao", new { @id = id });
                    }
                    else
                    {
                        sp.SoLuongTon = int.Parse(sp.SoLuongTon.ToString()) + int.Parse(item.SoLuong.ToString());
                        sp.LuotMua = int.Parse(sp.LuotMua.ToString()) - 1;

                    }
                }

            }
            db.SaveChanges();
            //var listCTDDH = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == id);
            //ViewBag.listCTDDH = listCTDDH;
            return RedirectToAction("DangGiao", "Admin");
        }

        [HttpPost]
        public ActionResult DuyetDonHangDangGiao(DONDATHANG ddh)
        {

            DONDATHANG luuddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDDH == ddh.MaDDH);

            luuddh.TinhTrangGiaoHang = "ht";
            luuddh.NgayGiao = DateTime.Now;
            luuddh.DaThanhToan = true;
            db.SaveChanges();
            var listCTDDH = db.CHITIETDONDATHANGs.Where(n => n.MaDDH == ddh.MaDDH);
            ViewBag.listCTDDH = listCTDDH;
            //GuiEmail("Xác nhận mua hàng Tshop", "tuantran.sw@gmail.com", "tshop.tuantran.sw@gmail.com", "abc@123Abc", "Đơn mua của bạn đã được xác nhận thành công. Đơn hàng sẽ được gửi đến trong vài ngày.");
            ViewBag.ThongBao = "Lưu đơn hàng thành công.";
            //ViewBag.ThongBao1 = "Gửi email thông báo cho khách hàng thành công.";
            return View(luuddh);
        }

        [Authorize(Roles = "7")]
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

        public void GuiEmail(string Title, string ToEmail, string FromEmail, string PassWord, string Content)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(ToEmail);
            mail.From = new MailAddress(ToEmail);
            mail.Subject = Title;
            mail.Body = Content;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            (FromEmail, PassWord);
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }

        public decimal ThongKeTongDoanhThu()
        {
            //decimal TongDoanhThu = db.CHITIETDONDATHANGs.Where(n=>n.DONDATHANG.TinhTrangGiaoHang=="dg").Sum(n => n.SoLuong * n.DonGia).Value;
            var listDDH = db.DONDATHANGs.Where(n => n.TinhTrangGiaoHang == "ht");
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.CHITIETDONDATHANGs.Sum(n => n.SoLuong * n.DonGia).Value.ToString());
            }

            return tongtien;
        }
        public decimal ThongKeDoanhThuThang(int Thang, int Nam)
        {
            var listDDH = db.DONDATHANGs.Where(n => n.NgayDat.Value.Month == Thang && n.NgayDat.Value.Year == Nam);
            decimal tongtien = 0;
            foreach (var item in listDDH)
            {
                tongtien += decimal.Parse(item.CHITIETDONDATHANGs.Sum(n => n.SoLuong * n.DonGia).Value.ToString());
            }

            return tongtien;
        }
        public double ThongKeDonHang()
        {
            double ddh = db.DONDATHANGs.Where(n => n.HuyDatHang == false && n.TinhTrangGiaoHang == "cxn" && n.Xoa == false).Count();
            return ddh;
        }
        public double ThongKeKhachHang()
        {
            double kh = db.KHACHHANGs.Count();
            return kh;
        }
        [Authorize(Roles = "8")]
        [HttpGet]
        public ActionResult TaiKhoan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TaiKhoan(THANHVIEN TV, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                THANHVIEN tv = db.THANHVIENs.SingleOrDefault(n => n.Email == TV.Email);
                THANHVIEN kh1 = db.THANHVIENs.SingleOrDefault(n => n.SoDienThoai == TV.SoDienThoai);
                if (tv == null && kh1 == null)
                {
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(TV.MatKhau);

                    TV.MatKhau = passwordHash;


                    db.THANHVIENs.Add(TV);
                    db.SaveChanges();
                    ViewBag.ThongBao = "Tạo tài khoản thành công";
                    Session["TaiKhoan"] = TV;
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
        [AllowAnonymous]
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string tTDN = f["username"].ToString();
            string tMK = f["pass"].ToString();

            THANHVIEN TV = db.THANHVIENs.SingleOrDefault(n => n.Email == tTDN);
            if (TV == null)
            {
                ViewBag.ThongBao1 = "Tên tài khoản hoặc mật khẩu không chính xác";
                return View("DangNhap");
            }
            string passwordHash = TV.MatKhau;
            if (BCrypt.Net.BCrypt.Verify(tMK, passwordHash) == true)
            {
                //Session["TaiKhoanAdmin"] = TV;
                IEnumerable<QUYENHANTHANHVIEN> listQH = db.QUYENHANTHANHVIENs.Where(n => n.MaTV == TV.MaTV);
                string Quyen = "";
                foreach (var item in listQH)
                {
                    Quyen += item.QUYENHAN.MaQ + ",";
                }
                Quyen = Quyen.Substring(0, Quyen.Length - 1);
                ViewBag.TenAdmin = TV.TenTV;
                PhanQuyen(TV.Email, Quyen);
                DangNhapCookie(TV.TenTV,TV.MaTV);
                return RedirectToAction("Index");
            }
            ViewBag.ThongBao1 = "Tên tài khoản hoặc mật khẩu không chính xác";
            return View("DangNhap");
        }
        [ChildActionOnly]
        public void PhanQuyen(string TaiKhoan, string Quyen)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1, TaiKhoan, DateTime.Now, DateTime.Now.AddHours(3), false, Quyen, FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
        }
        public void DangNhapCookie(string Ten,int Id)
        {
            //FormsAuthentication.Initialize();
            //byte[] b= Encoding.Default.GetBytes(Ten);
            HttpCookie ck = new HttpCookie("TenDangNhap");
            string MaTV = Id.ToString();
            ck.Expires = DateTime.Now.AddHours(3);
            ck.Values.Add("Ten", HttpUtility.UrlEncode(Ten));
            ck.Values.Add("Id", HttpUtility.UrlEncode(MaTV));

            Response.Charset = "UTF-8";
            Response.Cookies.Add(ck);

        }

        [AllowAnonymous]
        public ActionResult DangXuat()
        {
            //Session["TaiKhoanAdmin"] = null;
            FormsAuthentication.SignOut();
            Response.Cookies["TenDangNhap"].Expires = DateTime.Now.AddHours(-3);
            return RedirectToAction("DangNhap");
        }

        public ActionResult TatCaThanhVien(int? Page)
        {
            // Số sản phẩm trên trang
            int PageSize = 10;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1

            var tv = db.THANHVIENs.Where(n => n.Xoa == false);
            return View(tv.OrderBy(n => n.MaTV).ToPagedList(PageNumber, PageSize));
        }

        public ActionResult PhanQuyenAD(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            THANHVIEN tv = db.THANHVIENs.SingleOrDefault(n => n.MaTV == id);
            if (tv == null)
            {
                return HttpNotFound();
            }
            //tv.Xoa = true;
            //ddh.HuyDatHang = true;

            var listQHTV = db.QUYENHANTHANHVIENs.Where(n => n.MaTV == id);
            var listQ = db.QUYENHANs;
            ViewBag.listQHTV = listQHTV;
            ViewBag.listQ = listQ;
            return View(tv);
        }
        [HttpPost]
        public ActionResult PhanQuyenAD(IEnumerable<QUYENHANTHANHVIEN> listQHTV, int? MaTV)
        {
            // phân quyền lại
            // xoá quyền đã có
            var listDPQ = db.QUYENHANTHANHVIENs.Where(n => n.MaTV == MaTV);
            if (listDPQ != null)
            {
                db.QUYENHANTHANHVIENs.RemoveRange(listDPQ);
            }

            foreach (var item in listQHTV)
            {
                if (int.Parse(item.MaQ.ToString()) != 0)
                {
                    db.QUYENHANTHANHVIENs.Add(item);
                }
            }

            //tv.Xoa = true;
            //ddh.HuyDatHang = true;
            db.SaveChanges();
            ViewBag.ThongBao = "Lưu quyền thành công.";
            THANHVIEN tv = db.THANHVIENs.SingleOrDefault(n => n.MaTV == MaTV);
            if (tv == null)
            {
                return HttpNotFound();
            }
            var listQHTVM = db.QUYENHANTHANHVIENs.Where(n => n.MaTV == MaTV);
            var listQ = db.QUYENHANs;
            ViewBag.listQHTV = listQHTVM;
            ViewBag.listQ = listQ;
            return View(tv);
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