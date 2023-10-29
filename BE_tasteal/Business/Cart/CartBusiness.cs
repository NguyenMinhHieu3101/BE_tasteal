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

        public IEnumerable<CartEntity> GetCartById(int accountId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateServingSize(int CardId, int servingSize)
        {
            return _cartRepo.UpdateServingSize(CardId, servingSize);
        }
    }
}
