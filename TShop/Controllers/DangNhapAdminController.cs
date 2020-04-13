//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Security;
//using TShop.Models;
//namespace TShop.Controllers
//{
//    public class DangNhapAdminController : Controller
//    {
//        TShopEntities db = new TShopEntities();
//        // GET: DangNhapAdmin
//        [HttpGet]
//        public ActionResult Index()
//        {
//            return View();
//        }
//        [HttpPost]
//        public ActionResult Index(FormCollection f)
//        {
//            string tTDN = f["username"].ToString();
//            string tMK = f["pass"].ToString();

//            THANHVIEN TV = db.THANHVIENs.SingleOrDefault(n => n.Email == tTDN);
//            if (TV == null)
//            {
//                ViewBag.ThongBao1 = "Tên tài khoản hoặc mật khẩu không chính xác";
//                return View("Index");
//            }
//            string passwordHash = TV.MatKhau;
//            if (BCrypt.Net.BCrypt.Verify(tMK, passwordHash) == true)
//            {
//                Session["TaiKhoanAdmin"] = TV;
//                IEnumerable<QUYENHANTHANHVIEN> listQH = db.QUYENHANTHANHVIENs.Where(n => n.MaTV == TV.MaTV);
//                string Quyen = "";
//                foreach (var item in listQH)
//                {
//                    Quyen += item.QUYENHAN.MaQ + ",";
//                }
//                Quyen = Quyen.Substring(0, Quyen.Length - 1);
//                PhanQuyen(TV.Email, Quyen);
//                return RedirectToAction("Index","Admin");
//            }
//            ViewBag.ThongBao1 = "Tên tài khoản hoặc mật khẩu không chính xác";
//            return View("Index");
//        }
//        public void PhanQuyen(string TaiKhoan, string Quyen)
//        {
//            FormsAuthentication.Initialize();
//            var ticket = new FormsAuthenticationTicket(1, TaiKhoan, DateTime.Now, DateTime.Now.AddHours(3), false, Quyen, FormsAuthentication.FormsCookiePath);
//            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
//            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
//            Response.Cookies.Add(cookie);
//        }


//        public ActionResult DangXuat()
//        {
//            //Session["TaiKhoaAdmin"] = null;
           
//            FormsAuthentication.SignOut();
//            return RedirectToAction("Index");
//        }
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                if (db != null)
//                    db.Dispose();
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}