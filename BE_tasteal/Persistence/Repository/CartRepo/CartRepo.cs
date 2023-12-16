using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Dapper;
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
                using (var connection = _connection.GetConnection())
                {
                    string sqlcart = @"
                    select * from cart
                    where id = @CARTID
                    ";
                    var cart = connection.QueryFirst<CartEntity>(sqlcart, new { CARTID = CardId });

                    string sqlCartItem = @"
                    select * from cart_item
                    where cartId = @CARTID
                    ";
                    var cartItem = connection.Query<Cart_ItemEntity>(sqlCartItem, new { CARTID = CardId }).ToList();

                    foreach(var item in cartItem)
                    {
                        string sqlingredient = @"
                        select * from recipe_ingredient
                        where recipe_id = @RECIPEID
                        and ingredient_id = @INGREDIENTID
                        ";
                        var itemIngredient = connection.QueryFirst<Recipe_IngredientEntity>(sqlingredient, 
                            new { RECIPEID = cart.recipeId, INGREDIENTID = item.ingredient_id});

                        string updateCartItem = @"
                        update cart_item
                        set amount = @AMOUNT
                        where cartId = @CARTID
                        and ingredient_id = @INGREDIENTID
                        ";
                        connection.Execute(updateCartItem, new
                        {
                            AMOUNT = itemIngredient.amount_per_serving * servingSize,
                            CARTID = cart.id,
                            INGREDIENTID = item.ingredient_id
                        });
                    }

                    string updateCart = @"
                        update cart
                        set serving_size = @SIZE
                        where cartId = @CARTID
                        ";
                    var updateItem = connection.Execute(updateCart, new
                    {
                        SIZE = servingSize
                    });

                    return true;
                }
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
        public bool UpdateBoughtItem(int cartId, int IngredientId, bool isBought)
        {
            try
            {
                var cartItem = _context.cart_ItemEntities
                    .FirstOrDefault(
                        ci => ci.cartId == cartId 
                        && ci.ingredient_id == IngredientId);

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
