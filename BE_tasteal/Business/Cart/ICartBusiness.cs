using BE_tasteal.Entity.Entity;

namespace BE_tasteal.Business.Cart
{
    public interface ICartBusiness
    {
        bool UpdateServingSize(int CardId,int servingSize);
        IEnumerable<CartEntity> GetCartByAccountId(int accountId);
        IEnumerable<Cart_ItemEntity> GetItemByCartId(List<int> cartIds);
        bool DeleteCart(int cartId);
        bool DeleleCartByAccountId(int accountId);
        bool UpdateBoughtItem(int cartItemId, bool isBought);
    }
}
