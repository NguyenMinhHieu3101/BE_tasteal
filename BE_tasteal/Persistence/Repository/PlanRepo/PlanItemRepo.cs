using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.PlanRepo
{
    public class PlanItemRepo : GenericRepository<Plan_ItemEntity>, IPlanItemRepo
    {
        public PlanItemRepo(MyDbContext context, ConnectionManager connectionManager) : base(context, connectionManager)
        {
        }

        public void DeleteAll(int planItemId)
        {
            var entitiesToDelete = _context.Plan_Item.Where(entity => entity.plan_id == planItemId);

            _context.Plan_Item.RemoveRange(entitiesToDelete);

            _context.SaveChanges();
        }
    }
}
