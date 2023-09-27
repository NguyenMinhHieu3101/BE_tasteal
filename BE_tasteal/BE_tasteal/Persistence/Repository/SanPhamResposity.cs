using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Interface;
using Dapper;

namespace BE_tasteal.Persistence.Repository
{
    public class SanPhamResposity : ISanPhamResposity
    {
        private readonly MyDbContext _context;
        private readonly ConnectionManager _connectionManager;
        public SanPhamResposity(MyDbContext context,
           ConnectionManager connectionManager)
        {
            _context = context;
            _connectionManager = connectionManager;
        }

        #region get data by dapper
        public async Task<List<SanPhamEntity>> GetAll()
        {
            using (var connection = _connectionManager.GetConnection())
            {
                string query = @"
                SELECT
                    sp.MaSanPham,
                    sp.MaLoaiSanPham,
                    sp.TenSanPham,
                    lsp.MaLoaiSanPham,
                    lsp.TenLoaiSanPham,
                    lsp.MoTa,
                    lsp.TrangThai
                FROM SanPham sp
                INNER JOIN LoaiSanPham lsp ON sp.MaLoaiSanPham = lsp.MaLoaiSanPham";

                var lookup = new Dictionary<int, SanPhamEntity>();

                connection.Query<SanPhamEntity, LoaiSanPhamEntity, SanPhamEntity>(
                   query,
                   (sanPham, loaiSanPham) =>
                   {
                       SanPhamEntity sanPhamEntity;

                       if (!lookup.TryGetValue(sanPham.MaSanPham, out sanPhamEntity))
                       {
                           sanPhamEntity = sanPham;
                           sanPhamEntity.LoaiSanPham = loaiSanPham;
                           lookup.Add(sanPham.MaSanPham, sanPhamEntity);
                       }
                       return sanPhamEntity;
                   },
                   splitOn: "MaLoaiSanPham");
                return lookup.Values.ToList();
            }

        }
        #endregion


        #region write/upadte by linq
        public async Task<SanPhamEntity> InsertAsync(SanPhamEntity entity)
        {
            _context.Attach(entity);
            var entityEntry = await _context.Set<SanPhamEntity>().AddAsync(entity);

            await _context.SaveChangesAsync();

            return entityEntry.Entity;
        }
        #endregion
    }
}
