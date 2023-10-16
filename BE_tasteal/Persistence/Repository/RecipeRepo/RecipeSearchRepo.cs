using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Interface.RecipeRepo;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Dapper;

namespace BE_tasteal.Persistence.Repository.RecipeRepo
{
    public class RecipeSearchRepo : GenericRepository<RecipeEntity>, IRecipeSearchRepo
    {
        public RecipeSearchRepo(MyDbContext context,
           ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }

        #region get data by dapper
        public async Task<List<RecipeEntity>> Search(RecipeSearchDto data)
        {
            using (var connection = _connection.GetConnection())
            {

                string query = @"
                SELECT DISTINCT *
                FROM recipe AS r
                INNER JOIN Nutrition_Info AS n_i ON r.recipe_id = n_i.recipe_id
                INNER JOIN Ingredient AS i ON n_i.nutrition_info_id = i.nutrition_info_id
                INNER JOIN Account AS a ON a.id = r.author
                INNER JOIN Recipe_Occasion AS r_o ON r.recipe_id = r_o.recipe_id
                WHERE 
                (@IngredientID IS NULL OR i.ingredient_id IN @IngredientID) AND
                (@ExceptIngredientID IS NULL OR i.ingredient_id NOT IN @ExceptIngredientID) AND
                (@TotalTime IS NULL OR r.totalTime <= @TotalTime) AND
                (@ActiveTime IS NULL OR r.active_time <= @ActiveTime) AND
                (@OccasionID IS NULL OR r_o.occasion_id = @OccasionID) AND
                (@Calories IS NULL OR n_i.calories <= @Calories) AND
                ((@TextSearch IS NULL) OR
                (r.name LIKE '%' + @TextSearch + '%' OR
                r.introduction LIKE '%' + @TextSearch + '%' OR
                a.username LIKE '%' + @TextSearch + '%' OR 
                i.name LIKE '%' + @TextSearch + '%'))"
                ;

                var result = connection.Query<RecipeEntity>(query, new
                {
                    data.IngredientID,
                    data.ExceptIngredientID,
                    data.TotalTime,
                    data.ActiveTime,
                    data.OccasionID,
                    data.Calories,
                    data.TextSearch
                });

                return result.ToList();
            }

        }
        #endregion
    }
}
