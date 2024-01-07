using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Dapper;

namespace BE_tasteal.Persistence.Repository.CartItemRepo
{
    public class CartItemRepo : GenericRepository<Cart_ItemEntity>, ICartItemRepo
    {
        public CartItemRepo(MyDbContext context, ConnectionManager connectionManager) : base(context, connectionManager)
        {
        }
        public async Task<bool> addRecipeToCart(RecipeToCartReq req)
        {
            using (var connection = _connection.GetConnection())
            {
                foreach (var recipe_id in req.recipe_ids)
                {
                    var query = "select * from cart where accountId = @account_id and recipeId = @recipe_id";
                    var cart = await connection.QueryFirstOrDefaultAsync<CartEntity>(query, new
                    {
                        account_id = req.account_id,
                        recipe_id = recipe_id,
                    });
                    if (cart != null)
                    {
                        continue;
                    }

                    query = "select * from recipe where id = @recipe_id ";
                    var recipe = await connection.QueryFirstOrDefaultAsync<RecipeEntity>(query, new
                    {
                        recipe_id = recipe_id,
                    });

                    query = "insert into cart (accountId,recipeId,serving_size) values (@account_id,@recipe_id,@serving_size)";
                    _ = await connection.ExecuteAsync(query, new
                    {
                        account_id = req.account_id,
                        recipe_id = recipe_id,
                        serving_size = recipe.serving_size,
                    });

                    query = "select * from cart where accountId = @account_id and recipeId = @recipe_id";
                    cart = await connection.QueryFirstOrDefaultAsync<CartEntity>(query, new
                    {
                        account_id = req.account_id,
                        recipe_id = recipe_id,
                    });


                    query = "select * from recipe_ingredient where recipe_id = @recipe_id";
                    var items = await connection.QueryAsync<Recipe_IngredientEntity>(query, new
                    {
                        recipe_id = recipe_id,
                    });
                    Recipe_IngredientEntity[] recipe_ingredients = items.ToArray();

                    foreach (var recipe_ingredient in recipe_ingredients)
                    {
                        query = "insert into cart_item (cartId,ingredient_id,amount,isBought) values (@cart_id,@ingredient_id,@amount, @is_bought)";
                        _ = await connection.ExecuteAsync(query, new
                        {
                            cart_id = cart.id,
                            ingredient_id = recipe_ingredient.ingredient_id,
                            amount = recipe_ingredient.amount_per_serving * recipe.serving_size,
                            is_bought = false,
                        });
                    }
                }
                return true;
            }
        }
    }
}
