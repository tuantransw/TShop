using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TShop.Models
{
    [MetadataTypeAttribute(typeof(KHACHHANGMD))]
    public partial class  KHACHHANG
    {
        internal sealed class KHACHHANGMD
        {
  
            public int MaKH { get; set; }
            [DisplayName("Tên khách hàng")]
            [Required(ErrorMessage = "Không được để trống")]
            public string TenKH { get; set; }
            [Required(ErrorMessage = "Không được để trống")]
            public string DiaChi { get; set; }
            [Required(ErrorMessage = "Không được để trống")]
            [RegularExpression(@"0(3\d{8}|5\d{8}|7\d{8}|8\d{8}|9\d{8})", ErrorMessage = "Số điện thoại không hợp lệ")]
            public string SoDienThoai { get; set; }
            [Required(ErrorMessage = "Không được để trống")]
            [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Không được để trống")]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,255}$", ErrorMessage = "Mật khẩu phải từ 8 kí tự. Mật khẩu phải có ít nhất 1 chữ viết hoa, 1 số và 1 kí tự đặc biệt.")]
            public string MatKhau { get; set; }
            public Nullable<System.DateTime> NgaySinh { get; set; }
      
        }
    }
}