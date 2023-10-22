using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.SamPhamTemplate
{
    public interface ISanPhamResposity : IGenericRepository<SanPhamEntity>
    {
        Task<List<SanPhamEntity>> GetAll();
    }
}
