using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.CartItemRepo
{
    public interface ICartItemRepo : IGenericRepository<Cart_ItemEntity>
    {
        Task<bool> addRecipeToCart(RecipeToCartReq req);
    }
}
