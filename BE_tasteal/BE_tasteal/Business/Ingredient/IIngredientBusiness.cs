using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Business.Ingredient
{
    public interface IIngredientBusiness
    {
        List<IngredientEntity> AddFromExel(IFormFile excelFile);
    }
}
