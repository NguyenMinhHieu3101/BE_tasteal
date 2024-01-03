using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.PlanRepo
{
    public interface IPlanItemRepo : IGenericRepository<Plan_ItemEntity>
    {
        void DeleteAll(int planId);
    }
}
