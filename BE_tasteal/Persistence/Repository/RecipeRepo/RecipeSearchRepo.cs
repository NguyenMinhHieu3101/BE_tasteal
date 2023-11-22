using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.GenericRepository;
using BE_tasteal.Utils;
using Dapper;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Data.Common;
using System.Globalization;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BE_tasteal.Persistence.Repository.RecipeRepo
{
    public class RecipeSearchRepo : GenericRepository<RecipeEntity>, IRecipeSearchRepo
    {
        public RecipeSearchRepo(MyDbContext context,
           ConnectionManager connectionManager) : base(context, connectionManager)
        {

        }
        public async Task<List<RecipeEntity>> Search(RecipeSearchReq input)
        {
            using (var connection = _connection.GetConnection())
            {
                string sql = @"SELECT r.*
                    FROM Recipe r
                    JOIN Recipe_Ingredient ri ON r.id = ri.recipe_id
                    JOIN Ingredient i ON ri.ingredient_id = i.id
                   "
                ;

                var conditions = new List<string>();
                var parameters = new DynamicParameters();

                if (input.IngredientID?.Any() == true)
                {
                    conditions.Add("i.id IN @IngredientIds");
                    parameters.Add("IngredientIds", input.IngredientID);
                }

                if (input.ExceptIngredientID?.Any() == true)
                {
                    conditions.Add("i.id NOT IN @ExceptIngredientIds");
                    parameters.Add("ExceptIngredientIds", input.ExceptIngredientID);
                }

                if (input.OccasionID?.Any() == true)
                {
                    
                    conditions.Add("ro.occasion_id IN @OccasionIds");
                    parameters.Add("OccasionIds", input.OccasionID);
                }

                if(input.KeyWords?.Any() == true)
                {
                    string keywordSql = "  ";
                    List<string> keywordCondition = new List<string>();
                    foreach (var pattern in input.KeyWords)
                    {
                        keywordCondition.Add($"LOWER(introduction) REGEXP LOWER(@Pattern)");
                        parameters.Add("Pattern", pattern);
                    }
                    keywordSql += " ( " + string.Join(" OR ", keywordCondition) + " ) ";
                    conditions.Add(keywordSql);
                }
                

                if (conditions.Any())
                {
                    sql += " WHERE " + string.Join(" AND ", conditions) + " GROUP BY r.id ";
                }



                var result = await connection.QueryAsync<RecipeEntity>(sql, parameters);
               
                return result.ToList();
            }

        }
       
    }
}

//search Binh
//string query = @"
//                SELECT DISTINCT *
//                FROM recipe AS r
//                INNER JOIN Nutrition_Info AS n_i ON r.nutrition_info_id = n_i.id
//                INNER JOIN Ingredient AS i ON n_i.id = i.nutrition_info_id
//                INNER JOIN Account AS a ON a.id = r.author
//                INNER JOIN Recipe_Occasion AS r_o ON r.id= r_o.recipe_id
//                WHERE 
//                (@IngredientID IS NULL OR i.id IN @IngredientID) AND
//                (@ExceptIngredientID IS NULL OR i.id NOT IN @ExceptIngredientID) AND
//                (@TotalTime IS NULL OR r.totalTime <= @TotalTime) AND
//                (@ActiveTime IS NULL OR r.active_time <= @ActiveTime) AND
//                (@OccasionID IS NULL OR r_o.occasion_id IN @OccasionID) AND           
//		        (@KeyWords IS NULL OR REGEXP_LIKE(r.introduction, @KeyWordsFormat)) AND
//                ((@TextSearch IS NULL) OR
//                (r.name LIKE '%' + @TextSearch + '%' OR
//                r.introduction LIKE '%' + @TextSearch + '%' OR
//                a.username LIKE '%' + @TextSearch + '%' OR 
//                i.name LIKE '%' + @TextSearch + '%'))";
//(@Calories IS NULL OR n_i.calories <= @Calories) AND
//(@Calories IS NULL OR n_i.calories <= @Calories) AND
//var result = connection.Query<RecipeEntity>(query, new
//{
//    IngredientID = data.IngredientID,
//    ExceptIngredientID = data.ExceptIngredientID,
//    TotalTime = data.TotalTime,
//    ActiveTime = data.ActiveTime,
//    OccasionID = data.OccasionID,
//    TextSearch = data.TextSearch,
//    KeyWords = data.KeyWords,
//    KeyWordsFormat = data.KeyWordsFormat,
//});

//return result.ToList();


