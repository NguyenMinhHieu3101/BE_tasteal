using BE_tasteal.Entity.Entity;
using System.Numerics;

namespace BE_tasteal.Business.Cart
{
    public class CartBusiness: ICartBusiness
    {
        public CartBusiness() 
        {
            
        }

        public IEnumerable<CartEntity> GetCartById(int accountId)
        {
            throw new NotImplementedException();
        }
    }
}
