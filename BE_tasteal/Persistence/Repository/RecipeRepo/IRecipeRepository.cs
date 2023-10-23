using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.RecipeRepo
{
    public interface IRecipeRepository : IGenericRepository<RecipeEntity>
    {
        Task InsertRecipeIngredient(RecipeEntity recipe, List<IngredientEntity> ingredients);
        Task InsertDirection(RecipeEntity recipe, List<RecipeDirectionDto> direction);
        Task UpdateNutrition(RecipeEntity recipe, List<IngredientEntity> ingredients);
        List<RecipeEntity> GetRecipesWithIngredientsAndNutrition();
        Task<List<RecipeEntity>> RecipeByTime(PageFilter filter);
        Task<List<RecipeEntity>> RecipeByRating(PageFilter filter);
    }
}
