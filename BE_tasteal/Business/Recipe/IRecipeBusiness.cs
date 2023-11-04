using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Business.Recipe
{
    public interface IRecipeBusiness<T, U> where T : class where U : class
    {
        Task<List<U?>> GetAll();
        Task<U?> Add(T entity);
        Task<List<RecipeEntity>> AddFromExelAsync(IFormFile file);
        Task<List<U>?> Search(RecipeSearchDto option);
        List<RecipeEntity> GetAllRecipe();
        Task<RecipeRes> RecipeDetail(int id);
    }
}
