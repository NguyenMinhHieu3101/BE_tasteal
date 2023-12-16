using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Business.Cart
{
    public interface ICartBusiness
    {
        bool UpdateServingSize(int CardId,int servingSize);
        IEnumerable<CartEntity> GetCartByAccountId(string accountId);
        IEnumerable<Cart_ItemEntity> GetItemByCartId(List<int> cartIds);
        bool DeleteCart(int cartId);
        bool DeleleCartByAccountId(string accountId);
        bool UpdateBoughtItem(int cartItemId, int ingredientId, bool isBought);
    }
}
