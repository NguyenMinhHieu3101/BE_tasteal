using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Repository.AuthorRepo;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Dapper;

namespace BE_tasteal.Persistence.Repository.RecipeRepo
{
    public class RecipeSearchRepo : GenericRepository<RecipeEntity>, IRecipeSearchRepo
    {
        private readonly IUserRepo _userRepo;
        public RecipeSearchRepo(MyDbContext context,
           ConnectionManager connectionManager,
           IUserRepo userRepo) : base(context, connectionManager)
        {
            _userRepo = userRepo;
        }
        public async Task<List<RecipeSearchRes>> Search(RecipeSearchReq input)
        {
            using (var connection = _connection.GetConnection())
            {
                string sql = @"SELECT distinct r.*
                    FROM Recipe r
                    LEFT JOIN Recipe_Ingredient ri ON r.id = ri.recipe_id
                    LEFT JOIN Ingredient i ON ri.ingredient_id = i.id
                    LEFT JOIN Nutrition_info ni on ni.id = r.nutrition_info_id 
                    LEFT JOIN recipe_occasion ro on ro.recipe_id = r.id
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

                if (input.TotalTime.HasValue)
                {

                    conditions.Add("r.totalTime <= @TotalTime");
                    parameters.Add("TotalTime", input.TotalTime);
                }

                if (input.KeyWords?.Any() == true)
                {
                    string keywordSql = "  ";
                    List<string> keywordCondition = new List<string>();

                    int index = 0;

                    foreach (var pattern in input.KeyWords)
                    {
                        string parameterName = $"Pattern{index}";
                        keywordCondition.Add($" LOWER(introduction) REGEXP LOWER(@{parameterName}) ");
                        keywordCondition.Add($" LOWER(r.name) REGEXP LOWER(@{parameterName}) ");
                        parameters.Add(parameterName, pattern);
                        index++;
                    }

                    keywordSql += " ( " + string.Join(" OR ", keywordCondition) + " ) ";
                    conditions.Add(keywordSql);
                }


                if (input.Calories != null)
                {

                    conditions.Add(@"( ni.calories > @MIN and ni.calories < @MAX )");
                    parameters.Add("MIN", input.Calories.min);
                    parameters.Add("MAX", input.Calories.max);
                }

                if (conditions.Any())
                {
                    sql += " WHERE " + string.Join(" AND ", conditions) + " GROUP BY r.id ";
                }

                int pageSize = input.pageSize ?? 0;
                int page = input.page ?? 0;
                int offset = (page - 1) * pageSize;
                sql += " LIMIT @offset, @pageSize";
                parameters.Add("offset", offset);
                parameters.Add("pageSize", pageSize);
                Console.WriteLine(sql);

                var recipe = await connection.QueryAsync<RecipeEntity>(sql, parameters);


                var result = new List<RecipeSearchRes>();
                foreach (var item in recipe)
                {
                    RecipeSearchRes recipeSearchRes = new RecipeSearchRes
                    {
                        id = item.id,
                        name = item.name,
                        rating = item.rating,
                        totalTime = item.totalTime,
                        active_time = item.active_time,
                        serving_size = item.serving_size,
                        introduction = item.introduction,
                        author_note = item.author_note,
                        is_private = item.is_private,
                        image = item.image,
                        author = item.author,
                        nutrition_info_id = item.nutrition_info_id,
                        createdAt = item.createdAt,
                        updatedAt = item.updatedAt,
                        account = item.account,
                        nutrition_info = item.nutrition_info,
                        ingredients = item.ingredients,
                        direction = item.direction,
                        calories = 0
                    };

                    string caloQuery = @"select * from nutrition_info where id =  @ID";
                    var nutri = await connection.QueryFirstAsync<Nutrition_InfoEntity>(caloQuery, new
                    {
                        ID = item.nutrition_info_id
                    });
                    recipeSearchRes.calories = nutri.calories;

                    var user = await _userRepo.FindByIdAsync(item.author);

                    recipeSearchRes.account = user;

                    result.Add(recipeSearchRes);
                }

                return result;
            }

        }
        //public async Task<List<RecipeSearchRes>> Search(RecipeSearchReq input)
        //{
        //    using (var connection = _connection.GetConnection())
        //    {
        //        var parameters = new DynamicParameters();
        //        var sqlBuilder = new StringBuilder();
        //        sqlBuilder.Append(@"
        //    SELECT r.id, r.name, r.rating, r.totalTime, r.active_time, r.serving_size,
        //           r.introduction, r.author_note, r.is_private, r.image, r.author,
        //           r.nutrition_info_id, r.createdAt, r.updatedAt, ni.calories,
        //           a.uid, a.name , a.avatar, a.introduction,
        //           a.link, a.slogan, a.quote
        //    FROM Recipe r
        //    LEFT JOIN Recipe_Ingredient ri ON r.id = ri.recipe_id
        //    LEFT JOIN Ingredient i ON ri.ingredient_id = i.id
        //    LEFT JOIN Nutrition_info ni ON ni.id = r.nutrition_info_id
        //    LEFT JOIN recipe_occasion ro ON ro.recipe_id = r.id
        //    LEFT JOIN Account a ON a.uid = r.author
        //");

        //        var conditions = new List<string>();

        //        if (input.IngredientID?.Any() == true)
        //        {
        //            conditions.Add("i.id IN @IngredientIds");
        //            parameters.Add("IngredientIds", input.IngredientID);
        //        }

        //        if (input.ExceptIngredientID?.Any() == true)
        //        {
        //            conditions.Add("i.id NOT IN @ExceptIngredientIds");
        //            parameters.Add("ExceptIngredientIds", input.ExceptIngredientID);
        //        }

        //        if (input.OccasionID?.Any() == true)
        //        {
        //            conditions.Add("ro.occasion_id IN @OccasionIds");
        //            parameters.Add("OccasionIds", input.OccasionID);
        //        }

        //        if (input.TotalTime.HasValue)
        //        {
        //            conditions.Add("r.totalTime <= @TotalTime");
        //            parameters.Add("TotalTime", input.TotalTime);
        //        }

        //        if (input.KeyWords?.Any() == true)
        //        {
        //            var keywordConditions = input.KeyWords.Select((pattern, index) =>
        //            {
        //                string parameterName = $"Pattern{index}";
        //                parameters.Add(parameterName, pattern);
        //                return $"(LOWER(r.introduction) LIKE LOWER(@{parameterName}) OR LOWER(r.name) LIKE LOWER(@{parameterName}))";
        //            });
        //            conditions.AddRange(keywordConditions);
        //        }

        //        if (input.Calories != null)
        //        {
        //            conditions.Add(@"(ni.calories > @MIN AND ni.calories < @MAX)");
        //            parameters.Add("MIN", input.Calories.min);
        //            parameters.Add("MAX", input.Calories.max);
        //        }

        //        if (conditions.Any())
        //        {
        //            sqlBuilder.Append(" WHERE ");
        //            sqlBuilder.Append(string.Join(" AND ", conditions));
        //        }

        //        int pageSize = input.pageSize ?? 0;
        //        int page = input.page ?? 0;
        //        int offset = (page - 1) * pageSize;
        //        sqlBuilder.Append($" GROUP BY r.id LIMIT {offset}, {pageSize}");

        //        Console.WriteLine(sqlBuilder.ToString());

        //        var recipes = await connection.QueryAsync<RecipeSearchRes, AccountEntity, RecipeSearchRes>(
        //            sqlBuilder.ToString(),
        //            (recipeSearchRes, account) =>
        //            {
        //                recipeSearchRes.account = account;
        //                return recipeSearchRes;
        //            },
        //            parameters,
        //            splitOn: "uid"
        //        );

        //        return recipes.ToList();
        //    }
        //}



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


