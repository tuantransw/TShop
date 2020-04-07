using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TShop.Models;
namespace TShop.Controllers
{
    public class GioHangController : Controller
    {
        TShopEntities db = new TShopEntities();
        public List<ITEMGIOHANG> LayGioHang()
        {
            List<ITEMGIOHANG> listItemGioHang = Session["ItemGioHang"] as List<ITEMGIOHANG>;
            if (listItemGioHang == null)
            {
                listItemGioHang = new List<ITEMGIOHANG>();
                Session["ItemGioHang"] = listItemGioHang;

            }
            return listItemGioHang;
        }
        //Cách 1: Load lại trang
        public ActionResult ThemGioHang(int MaSP)
        {
            //Kiểm tra sản phẩm
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            else
            {
                //Lấy giỏ hàng
                List<ITEMGIOHANG> listItemGioHang = LayGioHang();

                //Trường hợp 1: Nếu sản phẩm đã có trong giỏ hàng

                ITEMGIOHANG spCheck = listItemGioHang.SingleOrDefault(n => n.MaSP == MaSP);

                if (spCheck != null)
                {
                    if (sp.SoLuongTon < spCheck.SoLuong)
                    {
                        return View("ThongBao");
                    }
                    if (sp.DonGia != spCheck.DonGia)
                    {
                        listItemGioHang.Clear();
                        ITEMGIOHANG itemGHNew = new ITEMGIOHANG(MaSP);
                        listItemGioHang.Add(itemGHNew);
                        ViewBag.TongTien = TinhTongTien();
                        return View("XemGioHang", listItemGioHang);
                    }
                    spCheck.SoLuong++;
                    spCheck.ThanhTien = spCheck.DonGia * spCheck.SoLuong;
                    ViewBag.TongTien = TinhTongTien();
                    return View("XemGioHang", listItemGioHang);
                }
                //Trường hợp 2: Sản phẩm chưa có trong giỏ hàng
                ITEMGIOHANG itemGH = new ITEMGIOHANG(MaSP);
                if (sp.SoLuongTon < itemGH.SoLuong)
                {
                    return View("ThongBao");
                }
                listItemGioHang.Add(itemGH);
                ViewBag.TongTien = TinhTongTien();
                return View("XemGioHang", listItemGioHang);
            }

        }

        public double TinhTongSoLuong()
        {
            List<ITEMGIOHANG> listGioHang = Session["ItemGioHang"] as List<ITEMGIOHANG>;
            if (listGioHang == null)
            {
                return 0;
            }
            return listGioHang.Sum(n => n.SoLuong);
        }
        public decimal TinhTongTien()
        {
            List<ITEMGIOHANG> listGioHang = Session["ItemGioHang"] as List<ITEMGIOHANG>;

            if (listGioHang == null)
            {
                return 0;
            }
            return listGioHang.Sum(n => n.ThanhTien);
        }
        public ActionResult XemGioHang()
        {
            List<ITEMGIOHANG> listGioHang = LayGioHang();
            ViewBag.TongTien = TinhTongTien();
            return View(listGioHang);
        }

        public ActionResult GioHangPartial()
        {
            List<ITEMGIOHANG> listGioHang = LayGioHang();
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();

            return PartialView(listGioHang);
        }

        [HttpGet]
        public ActionResult SuaGioHang(int MaSP)
        {
            if (Session["ItemGioHang"] == null)
            {
                return RedirectToAction("index", "TrangChu");
            }

            //Kiểm tra sản phẩm có tồn tại trong cơ sở dữ liệu
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            //Lấy sản phẩm từ session
            List<ITEMGIOHANG> listGioHang = LayGioHang();
            ITEMGIOHANG sPCheck = listGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (sPCheck == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            //Nếu có
            ViewBag.GioHang = listGioHang;
            ViewBag.TongTien = TinhTongTien();
            return View(sPCheck);
        }
        [HttpPost]
        public ActionResult CapNhatGioHang(ITEMGIOHANG item, FormCollection f)
        {
            int sl = Convert.ToInt32(f["SoLuong"]);
            SANPHAM spCheck = db.SANPHAMs.Single(n => n.MaSP == item.MaSP);
            if (spCheck.SoLuongTon < sl)
            {
                return View("ThongBao");
            }
            List<ITEMGIOHANG> listGioHang = LayGioHang();
            ITEMGIOHANG itemGioHangUpDate = listGioHang.Find(n => n.MaSP == item.MaSP);
            itemGioHangUpDate.SoLuong = sl;
            itemGioHangUpDate.ThanhTien = itemGioHangUpDate.SoLuong * itemGioHangUpDate.DonGia;
            return RedirectToAction("XemGioHang");
        }

        public ActionResult XoaGioHang(int MaSP)
        {
            if (Session["ItemGioHang"] == null)
            {
                return RedirectToAction("index", "TrangChu");
            }

            //Kiểm tra sản phẩm có tồn tại trong cơ sở dữ liệu
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            //Lấy sản phẩm từ session
            List<ITEMGIOHANG> listGioHang = LayGioHang();
            ITEMGIOHANG sPCheck = listGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (sPCheck == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            listGioHang.Remove(sPCheck);
            return RedirectToAction("XemGioHang");
        }
        public ActionResult DatHang()
        {
            if (Session["ItemGioHang"] == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }


            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("TaiKhoan", "TrangChu");
            }
            else
            {
                KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;

                //khachHang.DiaChi = kh.DiaChi;
                //khachHang.SoDienThoai = kh.SoDienThoai;
                //khachHang.Email = kh.Email;
                //db.KHACHHANGs.Add(khachHang);
                //db.SaveChanges();
                DateTime dt = DateTime.Now;
                DONDATHANG donDatHang = new DONDATHANG();
                donDatHang.NgayDat = dt;
                donDatHang.MaKH = kh.MaKH;
                donDatHang.NgayGiao = null;
                donDatHang.TinhTrangGiaoHang = "cxn";
                donDatHang.DaThanhToan = false;
                donDatHang.HuyDatHang = false;
                donDatHang.Xoa = false;
                db.DONDATHANGs.Add(donDatHang);
                db.SaveChanges();

                // 

                List<ITEMGIOHANG> listGioHang = LayGioHang();

                foreach (var item in listGioHang)
                {
                    CHITIETDONDATHANG chiTietDonDatHang = new CHITIETDONDATHANG();
                    chiTietDonDatHang.MaDDH = donDatHang.MaDDH;
                    chiTietDonDatHang.MaSP = item.MaSP;
                    chiTietDonDatHang.TenSP = item.TenSP;
                    chiTietDonDatHang.SoLuong = item.SoLuong;
                    chiTietDonDatHang.DonGia = item.DonGia;

                    db.CHITIETDONDATHANGs.Add(chiTietDonDatHang);
                }
                db.SaveChanges();
                Session["ItemGioHang"] = null;
                return RedirectToAction("XemGioHang");
            }

        }

        public ActionResult TienHanhDatHang()
        {
            if (Session["ItemGioHang"] == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }


            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("TaiKhoan", "TrangChu");
            }

            KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
            var listKH = db.KHACHHANGs.Single(n => n.MaKH == kh.MaKH);

            List<ITEMGIOHANG> listGioHang = LayGioHang();
            ViewBag.KhachHang = listGioHang;
            ViewBag.TongTien = TinhTongTien();

            return View(listKH);
        }
        //[ValidateInput(false)]
        [HttpPost]
        public ActionResult CapNhatThongTinKhachHang(KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TienHanhDatHang");
            }
            return RedirectToAction("TienHanhDatHang");
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
