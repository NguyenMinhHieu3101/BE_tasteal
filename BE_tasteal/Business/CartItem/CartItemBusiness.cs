using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.AuthorRepo;
using BE_tasteal.Persistence.Repository.CartItemRepo;
using BE_tasteal.Persistence.Repository.PantryItemRepo;

namespace BE_tasteal.Business.PantryItem
{
    public class CartItemBusiness : ICartItemBusiness
    {
        private ICartItemRepo _cartItemRepo;
        public CartItemBusiness(ICartItemRepo cartItemRepo)
        {
            _cartItemRepo = cartItemRepo;
        }
        public async Task<bool> addRecipeToCart(RecipeToCartReq req)
        {
            return await _cartItemRepo.addRecipeToCart(req);
        }
    }
}
