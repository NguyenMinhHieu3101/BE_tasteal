using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Interface.GenericRepository;

namespace BE_tasteal.Persistence.Interface
{
    public interface ISanPhamResposity : IGenericRepository<SanPhamEntity>
    {
        Task<List<SanPhamEntity>> GetAll();
    }
}
