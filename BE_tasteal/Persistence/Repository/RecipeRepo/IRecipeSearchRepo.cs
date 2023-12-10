using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.RecipeRepo
{
    public interface IRecipeSearchRepo : IGenericRepository<RecipeEntity>
    {
        Task<List<RecipeEntity>> Search(RecipeSearchReq data);
    }
}
