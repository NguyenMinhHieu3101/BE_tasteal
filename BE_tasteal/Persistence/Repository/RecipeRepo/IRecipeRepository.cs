using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
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
        IEnumerable<RecipeEntity> RecipeByRating(PageFilter filter);
        IEnumerable<RecipeEntity> RecipeByTime(PageFilter filter);
        IEnumerable<RelatedRecipeRes> GetRelatedRecipeByAuthor(int id);
    }
}
