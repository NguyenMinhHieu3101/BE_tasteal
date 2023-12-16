using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.CartRepo;
using System.Numerics;

namespace BE_tasteal.Business.Cart
{
    public class CartBusiness: ICartBusiness
    {
        private readonly ICartRepo _cartRepo;
        public CartBusiness(ICartRepo cartRepo) 
        {
            _cartRepo = cartRepo;
        }

        public IEnumerable<CartEntity> GetCartByAccountId(string accountId)
        {
            return _cartRepo.GetCartByAccountId(accountId);
        }
        public IEnumerable<Cart_ItemEntity> GetItemByCartId(List<int> cartIds)
        {
            return _cartRepo.GetItemByCartId(cartIds);
        }
        public bool UpdateServingSize(int CardId, int servingSize)
        {
            return _cartRepo.UpdateServingSize(CardId, servingSize);
        }
        public bool DeleteCart(int cartId)
        {
            return (_cartRepo.DeleteCart(cartId));
        }
        public bool DeleleCartByAccountId(string accountId)
        {
            return _cartRepo.DeleleCartByAccountId(accountId);
        }
        public bool UpdateBoughtItem(int cartId, int ingredientId, bool isBought)
        {
            return _cartRepo.UpdateBoughtItem(cartId, ingredientId, isBought);    
        }
    }
}
