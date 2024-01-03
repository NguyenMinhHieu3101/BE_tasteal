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
        public List<int>? getListIdOccasionByRecipeId(int recipeId)
        {
            var list = _context.Recipe_Occasion
                        .Where(c => c.recipe_id == recipeId)
                        .Select(c => c.occasion_id).ToList();
            return list;
        }
    }
}
