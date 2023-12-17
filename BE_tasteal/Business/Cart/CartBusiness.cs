using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.AuthorRepo;
using BE_tasteal.Persistence.Repository.CartRepo;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace BE_tasteal.Business.Cart
{
    public class CartBusiness: ICartBusiness
    {
        private readonly ICartRepo _cartRepo;
        private readonly IUserRepo _userRepo;
        public CartBusiness(
            ICartRepo cartRepo, 
            IUserRepo userRepo) 
        {
            _cartRepo = cartRepo;
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<CartEntity>?> GetCartByAccountId(string accountId)
        {
            var isAccountValid = await _userRepo.FindByIdAsync(accountId);
            if (isAccountValid == null)
            {
                return null;
            }
            return _cartRepo.GetCartByAccountId(accountId);
        }
        public IEnumerable<PersonalCartItemEntity> GetPersonalCartItemsWithIngredients(string accountId)
        {
            return _cartRepo.GetPersonalCartItemsWithIngredients(accountId);
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
        public async Task<bool> PostPersonalCartItem(PersonalCartItemReq request)
        {
            var result = await _cartRepo.PostPersonalCartItem(request);
            return result;
        }
        public async Task<bool> PutPersonalCartItem(PersonalCartItemUpdateReq request)
        {
            var result = await _cartRepo.PutPersonalCartItem(request);
            return result;
        }

        Task<bool> ICartBusiness.UpdateServingSize(int CardId, int servingSize)
        {
            throw new NotImplementedException();
        }
    }
}
