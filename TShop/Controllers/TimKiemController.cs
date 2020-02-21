using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TShop.Models;
using PagedList;
namespace TShop.Controllers
{
    public class TimKiemController : Controller
    {
        TShopEntities db = new TShopEntities();
        // GET: TimKiem
        public ActionResult KetQuaTimKiem(string TuKhoa,int? Page)
        {

            var listSanPham = db.SANPHAMs.Where(n => n.TenSP.Contains(TuKhoa));
            // Chức năng phân trang
            // Số sản phẩm trên trang
            int PageSize = 3;
            // Trang hiện tại
            int PageNumber = (Page ?? 1);// Nếu không có giá trị thì bằng 1
            ViewBag.TuKhoa = TuKhoa;
            return View(listSanPham.OrderBy(n => n.TenSP).ToPagedList(PageNumber, PageSize));
        }


        public ActionResult KetQuaTimKiemPartial(string TuKhoa)
        {
            var listSanPham = db.SANPHAMs.Where(n => n.TenSP.Contains(TuKhoa));
            ViewBag.TuKhoa = TuKhoa;

            return PartialView(listSanPham.OrderBy(n=>n.DonGia));
        }
    }
}