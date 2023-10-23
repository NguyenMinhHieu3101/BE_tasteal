using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.OccasionRepo
{
    public interface IOccasionRepo : IGenericRepository<OccasionEntity>
    {
        IEnumerable<OccasionEntity> GetAll();
    }
}
