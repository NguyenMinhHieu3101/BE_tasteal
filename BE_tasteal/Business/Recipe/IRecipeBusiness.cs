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
        Task<List<U>?> Search(RecipeSearchReq option);
        Task<List<RecipeRes>> GetAllRecipe(PageReq page);
        Task<RecipeRes> RecipeDetail(int id);
        Task<List<KeyWordRes>> GetKeyWords();
    }
}
