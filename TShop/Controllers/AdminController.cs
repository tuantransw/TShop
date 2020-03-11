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
        public ActionResult Index()
        {
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
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ThemSanPham(SANPHAM sp, HttpPostedFileBase HinhAnh, HttpPostedFileBase HinhAnh4, HttpPostedFileBase HinhAnh2, HttpPostedFileBase HinhAnh3)
        {
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.MaLSP), "MaLSP", "TenLSP");
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX");
            if (sp.TenSP == null)
            {
                ViewBag.TenSP = "Nhập tên sản phẩm";
                return View();
            }
            if (HinhAnh != null)
            {
                var filename = Path.GetFileName(HinhAnh.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Upload = "Hình ảnh đã tồn tại";
                }
                else
                {
                    HinhAnh.SaveAs(path);
                    sp.HinhAnh = filename;
                    sp.HinhAnh1 = filename;
                }
            }
            else
            {
                sp.HinhAnh = null;
                sp.HinhAnh1 = null;
                ViewBag.HinhAnh = "Thêm ít nhất 1 hình ảnh";
                return View();
            }
            if (HinhAnh2 != null)
            {
                var filename2 = Path.GetFileName(HinhAnh2.FileName);
                var path2 = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename2);
                if (System.IO.File.Exists(path2))
                {
                    ViewBag.Upload2 = "Hình ảnh đã tồn tại";
                }
                else
                {
                    HinhAnh2.SaveAs(path2);
                    sp.HinhAnh2 = filename2;
                }
            }
            else
            {
                sp.HinhAnh2 = null;
            }
            if (HinhAnh3 != null)
            {
                var filename3 = Path.GetFileName(HinhAnh3.FileName);
                var path3 = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename3);
                if (System.IO.File.Exists(path3))
                {
                    ViewBag.Upload3 = "Hình ảnh đã tồn tại";
                }
                else
                {
                    HinhAnh3.SaveAs(path3);
                    sp.HinhAnh3 = filename3;
                }
            }
            else
            {
                sp.HinhAnh3 = null;
            }

            if (HinhAnh4 != null)
            {
                var filename4 = Path.GetFileName(HinhAnh4.FileName);
                var path4 = Path.Combine(Server.MapPath("~/Content/HinhAnh"), filename4);
                if (System.IO.File.Exists(path4))
                {
                    ViewBag.Upload4 = "Hình ảnh đã tồn tại";
                }
                else
                {
                    HinhAnh4.SaveAs(path4);
                    sp.HinhAnh4 = filename4;
                }
            }
            else
            {
                sp.HinhAnh4 = null;

            }
            ///
            SANPHAM SP = db.SANPHAMs.SingleOrDefault(n => n.TenSP == sp.TenSP);
            if (sp.TenSP == null || SP.TenSP != null)
            {
                ViewBag.TenSP = "Sản phẩm đã tồn tại";
            }
            else
            {
                sp.NgayCapNhat = DateTime.Now;
                sp.LuotMua = 0;
                sp.LuotXem = 0;
                sp.Xoa = false;
                db.SANPHAMs.Add(sp);
                db.SaveChanges();
                return RedirectToAction("XemSanPham");
            }
            ViewBag.ABC = "Thêm sản phẩm không thành công";
            return View();
        }

        public ActionResult SuaSanPham()
        {
            return View();
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
    }
}