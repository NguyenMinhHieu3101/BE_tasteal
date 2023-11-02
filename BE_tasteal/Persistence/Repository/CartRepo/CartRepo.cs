using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BE_tasteal.Persistence.Repository.CartRepo
{
    public class CartRepo: GenericRepository<CartEntity>, ICartRepo
    {
        public CartRepo(MyDbContext context,
        ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }
        public IEnumerable<CartEntity> GetCartByAccountId(string accountId)
        {
            var result = _context.cart.Where(u => u.accountId == accountId)
                .Include(c => c.account)
                .Include(c => c.recipe)
                .ToList();
            return result;
        }
        public IEnumerable<Cart_ItemEntity> GetItemByCartId(List<int> cartIds)
        {

            IEnumerable<Cart_ItemEntity> cartItems = _context.cart_ItemEntities
                .Where(ci => cartIds.Contains(ci.cartId))
                .Include(c => c.ingredient)
                .AsEnumerable();
            return cartItems;
        }
        public bool UpdateServingSize(int CardId,int servingSize)
        {
            try
            {
                var cart = _context.cart.FirstOrDefault(c => c.id == CardId);

                if (cart != null)
                {
                    var cartItem = _context.cart_ItemEntities
                        .Where(c => c.cartId == cart.id)
                        .Include(c => c.ingredient)
                        .ToList();

                    foreach (var item in cartItem)
                    {
                        item.amount = item.amount * servingSize / cart.serving_size;
                    }
                }

                cart.serving_size = servingSize;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteCart(int cartId) 
        {
            try
            {
                var cartItemsToDelete = _context.cart_ItemEntities
                                .Where(ci => ci.cartId == cartId)
                                .ToList();
                _context.cart_ItemEntities.RemoveRange(cartItemsToDelete);

                var cartToDelete = _context.cart
                            .FirstOrDefault(c => c.id == cartId);
                if (cartToDelete != null)
                {
                    // Xóa Cart
                    _context.cart.Remove(cartToDelete);
                }
                _context.SaveChanges();
                return  true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public bool DeleleCartByAccountId(string accountId)
        {
            try
            {
                // Lấy danh sách các Cart_Item cần xóa dựa trên accountId
                var cartItemsToDelete = _context.cart_ItemEntities
                                            .Where(ci => ci.cart.accountId == accountId)
                                            .ToList();

                // Xóa các Cart_Item
                _context.cart_ItemEntities.RemoveRange(cartItemsToDelete);

                // Lấy danh sách các Cart cần xóa dựa trên accountId
                var cartsToDelete = _context.cart
                                     .Where(c => c.accountId == accountId)
                                     .ToList();

                // Xóa các Cart
                _context.cart.RemoveRange(cartsToDelete);

                // Lưu các thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public bool UpdateBoughtItem(int cartItemId, bool isBought)
        {
            try
            {
                var cartItem = _context.cart_ItemEntities.FirstOrDefault(ci => ci.cartId == cartItemId);

                if (cartItem != null)
                {                   
                    cartItem.isBought = isBought;

                    _context.SaveChanges();
                }
                else
                {
                    // Xử lý trường hợp cartItem không tồn tại
                    return false;
                }

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
