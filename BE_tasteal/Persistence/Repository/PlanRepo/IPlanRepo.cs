using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.PlanRepo
{
    public interface IPlanRepo : IGenericRepository<PlanEntity>
    {
        List<Plan_ItemEntity> getListPlanItem(string uid);
        Task<bool> addRecipeToPlan(PlanReq req);
        Task<bool> deleteRecipePlan(PlanDeleteReq req);
    }
}
