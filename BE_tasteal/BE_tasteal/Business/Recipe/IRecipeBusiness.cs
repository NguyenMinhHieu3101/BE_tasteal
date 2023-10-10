using BE_tasteal.Entity.DTO.Request;

namespace BE_tasteal.Business.Recipe
{
    public interface IRecipeBusiness<T, U> where T : class where U : class
    {
        Task<List<U?>> GetAll();
        Task<U?> Add(T entity);
        Task<List<U>?> Search(RecipeSearchDto option);
    }
}
