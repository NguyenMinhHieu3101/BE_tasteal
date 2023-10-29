using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.GenericRepository;
using System.Data.Common;

namespace BE_tasteal.Persistence.Repository.CartRepo
{
    public interface ICartRepo: IGenericRepository<CartEntity>
    {
        IEnumerable<CartEntity> GetCartById(int accountId);
        public IEnumerable<Cart_ItemEntity> GetItemByCartId(List<int> cartIds);
        bool UpdateServingSize(int CardId, int servingSize);
        bool DeleteCart(int cardId);
        bool DeleleCartByAccountId(int accountId);
        bool UpdateBoughtItem(int cartItemId, bool isBought);
    }
}
