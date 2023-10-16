using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE_tasteal.Entity.Entity
{
    [Table("LoaiSanPham")]
    public class LoaiSanPhamEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaLoaiSanPham { get; set; }
        [Required]
        public string TenLoaiSanPham { get; set; }
        public string MoTa { get; set; }
        public string TrangThai { get; set; }
    }
}
