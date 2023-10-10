using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Interface.GenericRepository;

namespace BE_tasteal.Persistence.Interface.RecipeRepo
{
    public interface IRecipeSearchRepo : IGenericRepository<RecipeEntity>
    {
        Task<List<RecipeEntity>> Search(RecipeSearchDto data);
    }
}
