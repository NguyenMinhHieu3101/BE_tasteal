using System.ComponentModel.DataAnnotations;

namespace BE_tasteal.Entity.DTO.Request
{
    public class SanPhamDto
    {
        [Required]

        public int MaLoaiSanPham { get; set; }

        [Required]
        [MaxLength(50)]
        public string? TenSanPham { get; set; }
    }
}
