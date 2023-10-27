using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.IngredientRepo
{
    public interface IIngredientRepo : IGenericRepository<IngredientEntity>
    {
        bool IngredientTypeValid(string name);
        bool IngredientValid(string name);
        Task<Ingredient_TypeEntity> GetIngredientType(string name);
        Task<Nutrition_InfoEntity> InsertNutrition(Nutrition_InfoEntity nutrition);
        Task<List<IngredientEntity>> GetAllIngredient();
        Task<IngredientEntity> GetIngredientByName(string name);
        Task<IngredientEntity> InsertIngredient(IngredientEntity ingredient, bool flag = false);
        IEnumerable<IngredientRes> GetIngredientsByRecipeId(int recipeId);
    }
}
