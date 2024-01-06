using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;

namespace BE_tasteal.Persistence.Repository.CartRepo
{
    public interface ICartRepo : IGenericRepository<CartEntity>
    {
        IEnumerable<CartEntity> GetCartByAccountId(string accountId);
        public IEnumerable<Cart_ItemEntity> GetItemByCartId(List<int> cartIds);
        bool UpdateServingSize(int CardId, int servingSize);
        bool DeleteCart(int cardId);
        bool DeleleCartByAccountId(string accountId);
        bool UpdateBoughtItem(int cartId, int IngredientId, bool isBought);
        List<PersonalCartItemEntity> GetPersonalCartItemsWithIngredients(string accountId);
        Task<PersonalCartItemEntity?> PostPersonalCartItem(PersonalCartItemReq request);
        Task<bool> PutPersonalCartItem(PersonalCartItemUpdateReq request);
    }
}
