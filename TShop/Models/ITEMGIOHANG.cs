using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TShop.Models
{
    public class ITEMGIOHANG
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string HinhAnh { get; set; }
        public ITEMGIOHANG(int maSP, int sL)
        {
            using (TShopEntities db=new TShopEntities())
            {
                this.MaSP = maSP;
                SANPHAM sp= db.SANPHAMs.Single(n => n.MaSP == maSP);
                this.TenSP = sp.TenSP;
                this.HinhAnh = sp.HinhAnh;
                this.DonGia = sp.DonGia.Value;
                this.SoLuong = sL;
                this.ThanhTien = DonGia * SoLuong;


            }
        }
        public ITEMGIOHANG(int maSP)
        {
            using (TShopEntities db = new TShopEntities())
            {
                this.MaSP = maSP;
                SANPHAM sp = db.SANPHAMs.Single(n => n.MaSP == maSP);
                this.TenSP = sp.TenSP;
                this.HinhAnh = sp.HinhAnh;
                this.DonGia = sp.DonGia.Value;
                this.SoLuong = 1;
                this.ThanhTien = DonGia * SoLuong;

            }
        }
        public ITEMGIOHANG()
        {

        }
    }
}