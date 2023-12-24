using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;
using System.Threading.Tasks;

namespace BE_tasteal.Persistence.Repository.IngredientTypeRepo
{
    public interface IIngredientTypeRepo : IGenericRepository<Ingredient_TypeEntity>
    {
        Task<IEnumerable<Ingredient_TypeEntity>> GetAllIngredientType();
        Task<Ingredient_TypeEntity?> GetIngredientTypeById(int id);
        Task<Ingredient_TypeEntity?> CreateIngredientType(Ingredient_TypeEntity ingredientType);
        Task<Ingredient_TypeEntity> UpdateIngredientType(Ingredient_TypeEntity ingredientType);
        Task<Ingredient_TypeEntity?> DeleteIngredientType(int id);
    }
}
