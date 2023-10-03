using BE_tasteal.Entity.Entity;
using Microsoft.EntityFrameworkCore;

namespace BE_tasteal.Persistence.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }

        #region DB Set
        public DbSet<SanPhamEntity> sanPhams { get; set; }
        public DbSet<LoaiSanPhamEntity> loaiSanPhams { get; set; }

        #endregion

        #region model creating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* modelBuilder.Entity<Sp_Ts>().HasKey(p =>
                 new { p.MaSanPham, p.MaThongSo });
             base.OnModelCreating(modelBuilder);*/
        }
        #endregion
    }
}
