using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Business.IngredientType
{
    public interface IIngredientTypeBusiness
    {
        Task<IEnumerable<Ingredient_TypeEntity>> GetAllIngredientType();
        Task<Ingredient_TypeEntity?> GetIngredientTypeById(DAGIngredientTypeReq ingredientType);
        Task<Ingredient_TypeEntity?> CreateIngredientType(CreateIngredientTypeReq ingredientType);
        Task<Ingredient_TypeEntity> UpdateIngredientType(UpdateIngredientTypeReq ingredientType);
        Task<Ingredient_TypeEntity?> DeleteIngredientType(DAGIngredientTypeReq ingredientType);
    }
}
