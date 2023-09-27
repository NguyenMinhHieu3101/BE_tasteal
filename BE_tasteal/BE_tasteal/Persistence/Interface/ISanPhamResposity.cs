using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Persistence.Interface
{
    public interface ISanPhamResposity
    {
        Task<List<SanPhamEntity>> GetAll();

        Task<SanPhamEntity> InsertAsync(SanPhamEntity entity);
    }
}
