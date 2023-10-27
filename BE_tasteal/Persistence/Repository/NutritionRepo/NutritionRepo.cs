using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using BE_tasteal.Persistence.Repository.OccasionRepo;

namespace BE_tasteal.Persistence.Repository.NutritionRepo
{
    public class NutritionRepo : GenericRepository<Nutrition_InfoEntity>, INutritionRepo
    {
        public NutritionRepo(MyDbContext context,
         ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }
    }
}
