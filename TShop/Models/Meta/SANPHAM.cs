using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
namespace TShop.Models
{
    [MetadataTypeAttribute(typeof(SANPHAMMD))]
    public partial class SANPHAM
    {
        internal sealed class SANPHAMMD
        {
            public int MaSP { get; set; }
            [DisplayName("Tên sản phẩm xxx")]
            public string TenSP { get; set; }
            [DisplayName("Đơn giá")]
            public Nullable<decimal> DonGia { get; set; }
            [DisplayName("Cấu hình")]
            public string CauHinh { get; set; }
            [DisplayName("Mô tả")]
            public string MoTa { get; set; }
            [DisplayName("Hình ảnh 1")]
            public string HinhAnh { get; set; }
            [DisplayName("Số lượng tồn")]
            public Nullable<int> SoLuongTon { get; set; }
            [DisplayName("Lượt mua")]
            public Nullable<int> LuotMua { get; set; }
            [DisplayName("Lượt xem")]
            public Nullable<int> LuotXem { get; set; }
            [DisplayName("Ngày cập nhật")]
            public Nullable<System.DateTime> NgayCapNhat { get; set; }
            [DisplayName("Loại sản phẩm")]
            public Nullable<int> MaLSP { get; set; }
            [DisplayName("Nhà cung cấp")]
            public Nullable<int> MaNCC { get; set; }
            [DisplayName("Nhà sản xuất")]
            public Nullable<int> MaNSX { get; set; }
            [DisplayName("Xoá")]
            public Nullable<bool> Xoa { get; set; }
            public string HinhAnh1 { get; set; }
            [DisplayName("Hình ảnh 2")]
            public string HinhAnh2 { get; set; }
            [DisplayName("Hình ảnh 3")]
            public string HinhAnh3 { get; set; }
            [DisplayName("Hình ảnh 4")]

            public string HinhAnh4 { get; set; }
            [DisplayName("Sản phẩm mới")]
            public Nullable<bool> Moi { get; set; }
        }
    }
}