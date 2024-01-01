using BE_tasteal.Entity.DTO.Request;

namespace BE_tasteal.Business.PantryItem
{
    public interface ICartItemBusiness
    {
        Task<bool> addRecipeToCart(RecipeToCartReq req);
    }
}
