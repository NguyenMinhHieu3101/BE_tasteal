using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class LoaiSanPhamReq
    {
        [Required]
        public string TenLoaiSanPham { get; set; }
        [Required]
        [MaxLength(100)]
        public string MoTa { get; set; }
        [DefaultValue("HoatDong")]
        public string TrangThai { get; set; }
    }
}
