using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Interface;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Dapper;

namespace BE_tasteal.Persistence.Repository
{
    public class SanPhamResposity : GenericRepository<SanPhamEntity>, ISanPhamResposity
    {
        public SanPhamResposity(MyDbContext context,
           ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }

        #region get data by dapper
        public async Task<List<SanPhamEntity>> GetAll()
        {
            Console.WriteLine("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            using (var connection = _connection.GetConnection())
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
    }
}
