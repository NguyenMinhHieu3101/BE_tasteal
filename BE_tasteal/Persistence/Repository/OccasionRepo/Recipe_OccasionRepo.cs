using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.OccasionRepo
{
    public class Recipe_OccasionRepo : GenericRepository<Recipe_OccasionEntity>, IRecipe_OccasionRepo
    {
        public Recipe_OccasionRepo(MyDbContext context,
        ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }
    }
}
