//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LOAISANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOAISANPHAM()
        {
            this.SANPHAMs = new HashSet<SANPHAM>();
        }
    
        public int MaLSP { get; set; }
        public string TenLSP { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<int> MaLSPGoc { get; set; }
        public string MoTa { get; set; }
        public string BiDanh { get; set; }
        public Nullable<bool> Xoa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
