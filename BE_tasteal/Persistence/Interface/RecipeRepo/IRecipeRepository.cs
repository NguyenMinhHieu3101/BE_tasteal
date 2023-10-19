using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Interface.GenericRepository;

namespace BE_tasteal.Persistence.Interface.RecipeRepo
{
    public interface IRecipeRepository : IGenericRepository<RecipeEntity>
    {
        Task InsertRecipeIngredient(RecipeEntity recipe, List<IngredientEntity> ingredients);
        Task InsertDirection(RecipeEntity recipe, List<RecipeDirectionDto> direction);
        Task UpdateNutrition(RecipeEntity recipe, List<IngredientEntity> ingredients);
    }
}
