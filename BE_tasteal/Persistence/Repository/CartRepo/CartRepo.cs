using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Dapper;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;

namespace BE_tasteal.Persistence.Repository.CartRepo
{
    public class CartRepo : GenericRepository<CartEntity>, ICartRepo
    {
        public CartRepo(MyDbContext context,
        ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }
        public IEnumerable<CartEntity> GetCartByAccountId(string accountId)
        {
            var result = _context.Cart.Where(u => u.accountId == accountId)
                .Include(c => c.account)
                .Include(c => c.recipe)
                .ToList();
            return result;
        }
        public IEnumerable<Cart_ItemEntity> GetItemByCartId(List<int> cartIds)
        {

            IEnumerable<Cart_ItemEntity> cartItems = _context.Cart_Item
                .Where(ci => cartIds.Contains(ci.cartId))
                .Include(c => c.ingredient)
                .AsEnumerable();
            return cartItems;
        }
        public bool UpdateServingSize(int CardId, int servingSize)
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

                    foreach (var item in cartItem)
                    {
                        string sqlingredient = @"
                        select * from recipe_ingredient
                        where recipe_id = @RECIPEID
                        and ingredient_id = @INGREDIENTID
                        ";
                        var itemIngredient = connection.QueryFirst<Recipe_IngredientEntity>(sqlingredient,
                            new { RECIPEID = cart.recipeId, INGREDIENTID = item.ingredient_id });

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
                        where id = @CARTID
                        ";
                    var updateItem = connection.Execute(updateCart, new
                    {
                        SIZE = servingSize,
                        CARTID = CardId
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
                var cartItemsToDelete = _context.Cart_Item
                                .Where(ci => ci.cartId == cartId)
                                .ToList();
                _context.Cart_Item.RemoveRange(cartItemsToDelete);
                _context.SaveChanges();

                var cartToDelete = _context.Cart
                            .FirstOrDefault(c => c.id == cartId);
                if (cartToDelete != null)
                {
                    // Xóa Cart
                    _context.Cart.Remove(cartToDelete);
                }
                _context.SaveChanges();
                return true;
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
                var cartItemsToDelete = _context.Cart_Item
                                            .Where(ci => ci.cart.accountId == accountId)
                                            .ToList();
                _context.Cart_Item.RemoveRange(cartItemsToDelete);

                var personalCartItems = _context.PersonalCartItems
                                            .Where(ci => ci.account_id == accountId)
                                            .ToList();
                _context.PersonalCartItems.RemoveRange(personalCartItems);

                // Lấy danh sách các Cart cần xóa dựa trên accountId
                var cartsToDelete = _context.Cart
                                     .Where(c => c.accountId == accountId)
                                     .ToList();

                // Xóa các Cart
                _context.Cart.RemoveRange(cartsToDelete);

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
                var cartItem = _context.Cart_Item
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public List<PersonalCartItemEntity> GetPersonalCartItemsWithIngredients(string accountId)
        {
            var cartItemsWithIngredients = _context.PersonalCartItems
                .Where(item => item.account_id == accountId)
                .Include(item => item.ingredient)
                .ToList();

            return cartItemsWithIngredients;
        }
        public async Task<PersonalCartItemEntity?> PostPersonalCartItem(PersonalCartItemReq request)
        {
            try
            {
                IngredientEntity? ingredient;
                if (request.ingredient_id != null)
                    ingredient = await _context.Ingredient.FindAsync(request.ingredient_id);
                else
                    ingredient = null;

                PersonalCartItemEntity personalCartItem;
                if (ingredient == null)
                {
                    personalCartItem = new PersonalCartItemEntity
                    {
                        account_id = request.account_id,
                        amount = request.amount,
                        is_bought = request.is_bought,
                        name = request.name,
                    };
                }
                else
                {
                    personalCartItem = new PersonalCartItemEntity
                    {
                        ingredient_id = request.ingredient_id,
                        account_id = request.account_id,
                        amount = request.amount,
                        is_bought = request.is_bought
                    };
                }

                _context.Attach(personalCartItem);
                var entityEntry = await _context.Set<PersonalCartItemEntity>().AddAsync(personalCartItem);
                _context.SaveChanges();

                return entityEntry.Entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

        }
        public async Task<bool> PutPersonalCartItem(PersonalCartItemUpdateReq request)
        {
            try
            {
                var personalCartItem = await _context.PersonalCartItems.FindAsync(request.id);
                if (personalCartItem == null)
                {
                    return false;
                }

                personalCartItem.name = request.name;
                personalCartItem.amount = request.amount;
                personalCartItem.is_bought = request.is_bought;

                _context.Entry(personalCartItem).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
