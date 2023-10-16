using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Business.Ingredient
{
    public interface IIngredientBusiness
    {
        Task<List<IngredientEntity>> AddFromExelAsync(IFormFile file);
    }
}
