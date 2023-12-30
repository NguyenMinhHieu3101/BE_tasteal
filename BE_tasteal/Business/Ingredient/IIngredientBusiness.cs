using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Business.Ingredient
{
    public interface IIngredientBusiness
    {
        Task<List<IngredientEntity>> AddFromExelAsync(IFormFile file);
        Task<(List<IngredientEntity>, int)> GetAllIngredient(PageReq _page);

    }
}
