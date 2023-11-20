using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace BE_tasteal.Persistence.Repository.RecipeRepo
{
    public class RecipeRepository : GenericRepository<RecipeEntity>, IRecipeRepository
    {
        public RecipeRepository(MyDbContext context,
           ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }

        public async Task InsertRecipeIngredient(RecipeEntity recipe, List<IngredientEntity> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                Recipe_IngredientEntity newRecipeIngre = new Recipe_IngredientEntity
                {
                    recipe_id = recipe.id,
                    ingredient_id = ingredient.id,
                    amount_per_serving = ingredient.amount / recipe.serving_size,
                    //amount = ingredient.amount
                };
                _context.Attach(newRecipeIngre);
                await _context.Set<Recipe_IngredientEntity>().AddAsync(newRecipeIngre);
                await _context.SaveChangesAsync();

            }
        }
        public async Task InsertDirection(RecipeEntity recipe, List<RecipeDirectionReq> directions)
        {
            List<Recipe_DirectionEntity> listAdded = new List<Recipe_DirectionEntity>();
            foreach (var direction in directions)
            {
                Recipe_DirectionEntity newDirection = new Recipe_DirectionEntity
                {
                    recipe_id = recipe.id,
                    step = direction.step,
                    direction = direction.direction,
                    image = direction.image,
                };
                _context.Attach(newDirection);
                var newItem = await _context.Set<Recipe_DirectionEntity>().AddAsync(newDirection);
                await _context.SaveChangesAsync();
                listAdded.Add(newItem.Entity);
            }
            recipe.direction = listAdded;
        }
        public async Task UpdateNutrition(RecipeEntity recipe, List<IngredientEntity> ingredients)
        {
            Nutrition_InfoEntity newNutri = new Nutrition_InfoEntity
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
            foreach (var ingre in ingredients)
            {
                var nutritionOfIngre = ingre.nutrition_info;
                newNutri.calories += (nutritionOfIngre.calories * ingre.amount / 100M);
                newNutri.fat += (nutritionOfIngre.fat * ingre.amount / 100M);
                newNutri.saturated_fat += (nutritionOfIngre.saturated_fat * ingre.amount / 100M);
                newNutri.trans_fat += (nutritionOfIngre.trans_fat * ingre.amount / 100M);
                newNutri.cholesterol += (nutritionOfIngre.cholesterol * ingre.amount / 100M);
                newNutri.carbohydrates += (nutritionOfIngre.carbohydrates * ingre.amount / 100M);
                newNutri.fiber += (nutritionOfIngre.fiber * ingre.amount / 100M);
                newNutri.sugars += (nutritionOfIngre.sugars * ingre.amount / 100M);
                newNutri.protein += (nutritionOfIngre.protein * ingre.amount / 100M);
                newNutri.sodium += (nutritionOfIngre.sodium * ingre.amount / 100M);
                newNutri.vitaminD += (nutritionOfIngre.vitaminD * ingre.amount / 100M);
                newNutri.calcium += (nutritionOfIngre.calcium * ingre.amount / 100M);
                newNutri.iron += (nutritionOfIngre.iron * ingre.amount / 100M);
                newNutri.potassium += (nutritionOfIngre.potassium * ingre.amount / 100M);
            }


            _context.Attach(newNutri);
            var nutriAdded = await _context.Set<Nutrition_InfoEntity>().AddAsync(newNutri);
            await _context.SaveChangesAsync();

            recipe.nutrition_info_id = nutriAdded.Entity.id;
            _context.Attach(newNutri);
            await _context.SaveChangesAsync();
        }
        public List<RecipeEntity> GetRecipesWithIngredientsAndNutrition()
        {
            string sql = @"
                                  
                SELECT  r.id, r.name, r.rating, r.totalTime, r.active_time, r.serving_size, 
                        r.introduction, r.author_note, r.is_private, r.image, r.author, 
                        r.nutrition_info_id, r.createdAt, r.updatedAt, 
                        i.id AS IngredientId, i.name AS IngredientName, i.image AS IngredientImage, 
                        i.nutrition_info_id AS IngredientNutritionInfoId, i.type_id AS IngredientTypeId, 
                        i.isLiquid AS IsLiquid, i.ratio AS Ratio, 
                        ri.amount_per_serving AS IngredientAmount, ri.note AS IngredientNote, 
                        ni.id AS NutritionId, ni.calories AS Calories
                FROM recipe r
                LEFT JOIN recipe_ingredient ri ON r.id = ri.recipe_id
                LEFT JOIN ingredient i ON ri.ingredient_id = i.id
                LEFT JOIN nutrition_info ni ON i.nutrition_info_id = ni.id"
            ;

            using (var connection = _connection.GetConnection())
            {
                var recipeDictionary = new Dictionary<int, RecipeEntity>();
                connection.Query<RecipeEntity, IngredientEntity, Nutrition_InfoEntity, RecipeEntity>(
                    sql,
                     (recipe, ingredient, nutrition) =>
                     {
                         RecipeEntity recipeEntity;

                         if (!recipeDictionary.TryGetValue(recipe.id, out recipeEntity))
                         {
                             recipeEntity = recipe;
                             recipeEntity.ingredients = new List<IngredientEntity>();
                             recipeDictionary.Add(recipeEntity.id, recipeEntity);
                         }

                         if (ingredient != null)
                         {
                             ingredient.nutrition_info = nutrition;
                             recipeEntity.ingredients.Add(ingredient);
                         }

                         return recipeEntity;
                     },
                     splitOn: "IngredientId,NutritionId"
                    );
                return recipeDictionary.Values.ToList();
            }
        }
        public IEnumerable<RecipeEntity> RecipeByTime(PageFilter filter)
        {
            int pageSize = filter.pageSize;
            int page = filter.page;
            int offset = (page - 1) * pageSize;

            IQueryable<RecipeEntity> query = _context.recipe
                                               .Include(r => r.account)
                                               .Include(r => r.nutrition_info);
            if (filter.isDescend)
            {
                query = query.OrderByDescending(r => r.createdAt);
            }
            else
            {
                query = query.OrderBy(r => r.createdAt);
            }

            var recipes = query.Skip((page - 1) * pageSize)
                           .Take(pageSize)
                           .AsEnumerable();

            return recipes;
        }
        public IEnumerable<RecipeEntity> RecipeByRating(PageFilter filter)
        {
            int pageSize = filter.pageSize;
            int page = filter.page;
            int offset = (page - 1) * pageSize;


            IQueryable<RecipeEntity> query = _context.recipe
                                               .Include(r => r.account)
                                               .Include(r => r.nutrition_info);
            if (filter.isDescend)
            {
                query = query.OrderByDescending(r => r.rating);
            }
            else
            {
                query = query.OrderBy(r => r.rating);
            }

            var recipes = query.Skip((page - 1) * pageSize)
                           .Take(pageSize)
                           .AsEnumerable();

            return recipes;
        }
        public IEnumerable<RelatedRecipeRes> GetRelatedRecipeByAuthor(string id)
        {
            using (var connection = _connection.GetConnection())
            {
                string sql = @"
                select recipe.id, recipe.name, recipe.image, recipe.totalTime, recipe.rating,  count(recipe.id)  as ingredientAmoun  from recipe, account , recipe_ingredient
                where recipe.author = account.uid
                and recipe.id = recipe_ingredient.recipe_id
                and account.uid = @Id
                group by recipe.id
                limit 16
                ";

                var result = connection.Query<RelatedRecipeRes>(sql, new { Id = id });

                return result;
            }
        }
        public List<int> GetAllRecipeId(PageReq req)
        {
            using (var connection = _connection.GetConnection())
            {
                int pageSize = req.pageSize;
                int page = req.page;
                int offset = (page - 1) * pageSize;

                string sql = @"
                select id from recipe
                LIMIT @offset, @pageSize
                ";
                var result = connection.Query<int>(sql, new
                {
                    offset = offset,
                    pageSize = pageSize
                }).ToList();
                return result;
            }
        }
        public async Task<List<KeyWordRes>> GetKeyWords()
        {
            using (var connection = _connection.GetConnection())
            {
                string sql = @"
                SELECT keyword, COUNT(*) AS frequency
                FROM (
                    SELECT  word AS keyword
                    FROM (
                        SELECT  id, name, TRIM(SUBSTRING_INDEX(SUBSTRING_INDEX(introduction, ' ', n.digit + 1), ' ', -1)) AS word
                        FROM recipe
                        JOIN (
                            SELECT 0 AS digit UNION ALL SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3
                        ) n ON LENGTH(REPLACE(introduction, ' ', '')) <= LENGTH(introduction) - n.digit
                    ) words
                    WHERE LENGTH(word) > 0
                ) keyword_counts
                GROUP BY keyword
                ORDER BY frequency DESC;
                ";
                var result = await connection.QueryAsync<KeyWordRes>(sql);
                return result.ToList();
            }
        }
    }
}
