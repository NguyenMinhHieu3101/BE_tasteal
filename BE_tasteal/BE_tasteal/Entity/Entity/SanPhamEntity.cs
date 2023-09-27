using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("SanPham")]
    public class SanPhamEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaSanPham { get; set; }
        public int MaLoaiSanPham { get; set; }

        [Required]
        public string? TenSanPham { get; set; }

        [ForeignKey("MaLoaiSanPham")]
        public LoaiSanPhamEntity LoaiSanPham { get; set; }
    }
}
