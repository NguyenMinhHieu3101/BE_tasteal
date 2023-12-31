using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Dapper;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;

namespace BE_tasteal.Persistence.Repository.IngredientRepo
{
    public class IngredientRepo : GenericRepository<IngredientEntity>, IIngredientRepo
    {
        private readonly ILogger<IngredientEntity> _logger;
        public IngredientRepo(MyDbContext context,
          ConnectionManager connectionManager, ILogger<IngredientEntity> logger) : base(context, connectionManager)
        {
            _logger = logger;
        }

        public bool IngredientTypeValid(string name)
        {
            var type = _context.Ingredient_Type.FirstOrDefault(e => e.name == name);
            if (type != null)
            {
                return true;
            }
            return false;
        }
        public bool IngredientValid(string name)
        {
            var type = _context.Ingredient.FirstOrDefault(e => e.name == name);
            if (type != null)
            {
                return true;
            }
            return false;
        }

        public async Task<IngredientEntity> GetIngredientByName(string name)
        {
            return await _context.Ingredient
                .Include(e => e.nutrition_info)
                .FirstOrDefaultAsync(e => e.name == name);
        }
        public async Task<IngredientEntity> GetIngredientById(int id)
        {
            return await _context.Ingredient
                .Include(e => e.nutrition_info)
                .FirstOrDefaultAsync(e => e.id == id);
        }
        public async Task<Ingredient_TypeEntity> GetIngredientType(string name)
        {
            var type = _context.Ingredient_Type.FirstOrDefault(e => e.name == name);
            if (type != null)
            {
                return type;
            }
            var ingredient_Type = new Ingredient_TypeEntity
            {
                name = name,
            };
            _context.Attach(ingredient_Type);
            var entity = await _context.Set<Ingredient_TypeEntity>().AddAsync(ingredient_Type);

            await _context.SaveChangesAsync();
            _logger.LogInformation("Add new ingredient type: " + entity.Entity);
            return entity.Entity;
        }
        public async Task<Nutrition_InfoEntity> InsertNutrition(Nutrition_InfoEntity nutri)
        {
            _context.Attach(nutri);
            var entityEntry = await _context.Set<Nutrition_InfoEntity>().AddAsync(nutri);

            await _context.SaveChangesAsync();
            _logger.LogInformation("Add new nutrition info: " + entityEntry.Entity);
            return entityEntry.Entity;
        }
        public async Task<(List<IngredientEntity>, int)> GetAllIngredient(PageReq _page)
        {
            int pageNumber = _page.page;
            int pageSize = _page.pageSize;

            var totalIngredients = await _context.Ingredient.CountAsync();

            int totalPages = (int)Math.Ceiling(totalIngredients / (double)pageSize);

            var ingredientsWithType = await _context.Ingredient
                .Include(i => i.ingredient_type)
                .Include(i => i.nutrition_info)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (ingredientsWithType, totalPages);
        }
        /// <summary>
        /// func add new ingredient
        /// </summary>
        /// <param name="flag">flag true when input not in ingredient table</param>
        public async Task<IngredientEntity> InsertIngredient(IngredientEntity ingredient, bool flag = false)
        {
            if (flag)
            {
                var defaultNutrition = new Nutrition_InfoEntity
                {
                    calories = 0,
                    fat = 0,
                    saturated_fat = 0,
                    trans_fat = 0,
                    cholesterol = 0,
                    carbohydrates = 0,
                    fiber = 0,
                    sugars = 0,
                    protein = 0,
                    sodium = 0,
                    vitaminD = 0,
                    calcium = 0,
                    iron = 0,
                    potassium = 0,
                };
                _context.Attach(defaultNutrition);
                var newNutrition = await _context.Set<Nutrition_InfoEntity>().AddAsync(defaultNutrition);
                await _context.SaveChangesAsync();
                ingredient.nutrition_info_id = newNutrition.Entity.id;
            }
            _context.Attach(ingredient);
            var entityEntry = await _context.Set<IngredientEntity>().AddAsync(ingredient);

            await _context.SaveChangesAsync();

            _logger.LogInformation("Add new nutrition info: " + entityEntry.Entity);
            entityEntry.Entity.amount = ingredient.amount;
            return entityEntry.Entity;
        }
        public IEnumerable<IngredientRes> GetIngredientsByRecipeId(int recipeId)
        {
            using (var connection = _connection.GetConnection())
            {
                string sqlrecipe = @"SELECT * from recipe where id =@RECIPE";
                var recipe = connection.QueryFirst<RecipeEntity>(sqlrecipe, new
                {
                    RECIPE = recipeId
                });

                string sql = @"SELECT i.id, i.name, i.image, ri.amount_per_serving, i.isLiquid 
                      FROM Ingredient i
                      INNER JOIN Recipe_Ingredient ri ON i.id = ri.ingredient_id
                      WHERE ri.recipe_id = @RecipeId";

                var result = connection.Query<IngredientRes>(sql, new { RecipeId = recipeId });
                foreach (var item in result)
                {
                    item.amount = item.amount_per_serving * recipe.serving_size;
                }
                return result;
            }
        }

        public async Task deleteIngre_recipe(int recipe_id)
        {
            List<Recipe_IngredientEntity> ingredients = _context.Recipe_Ingredient.Where(s => s.recipe_id == recipe_id).ToList();
            foreach (var item in ingredients)
            {
                _context.Set<Recipe_IngredientEntity>().Remove(item);
                await _context.SaveChangesAsync();
            }

        }
    }
}
