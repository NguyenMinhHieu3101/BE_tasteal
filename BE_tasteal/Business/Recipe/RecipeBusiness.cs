﻿using AutoMapper;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.DTO.Response;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.AuthorRepo;
using BE_tasteal.Persistence.Repository.CommentRepo;
using BE_tasteal.Persistence.Repository.Direction;
using BE_tasteal.Persistence.Repository.IngredientRepo;
using BE_tasteal.Persistence.Repository.NutritionRepo;
using BE_tasteal.Persistence.Repository.RecipeRepo;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System;
using System.Text.RegularExpressions;

namespace BE_tasteal.Business.Recipe
{
    public class RecipeBusiness : IRecipeBusiness<RecipeReq, RecipeEntity>
    {
        private readonly IMapper _mapper;
        private readonly IRecipeRepository _recipeResposity;
        private readonly IRecipeSearchRepo _recipeSearchRepo;
        private readonly IIngredientRepo _ingredientRepo;
        private readonly IAuthorRepo _authorRepo;
        private readonly INutritionRepo _nutritionRepo;
        private readonly IDirectionRepo _directionRepo;
        private readonly ICommentRepo _commentRepo;
        private readonly ILogger<RecipeEntity> _logger;
        public RecipeBusiness(IMapper mapper,
           IRecipeRepository recipeResposity,
           IRecipeSearchRepo recipeSearchRepo,
           IIngredientRepo ingredientRepo,
           IAuthorRepo authorRepo,
           INutritionRepo nutritionRepo,
           IDirectionRepo directionRepo,
           ICommentRepo commentRepo,
            ILogger<RecipeEntity> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _recipeResposity = recipeResposity;
            _recipeSearchRepo = recipeSearchRepo;
            _ingredientRepo = ingredientRepo;
            _authorRepo = authorRepo;
            _nutritionRepo = nutritionRepo;
            _directionRepo = directionRepo;
            _commentRepo = commentRepo;
        }
        public async Task<RecipeEntity?> Add(RecipeReq entity)
        {
            var newRecipeEntity = _mapper.Map<RecipeEntity>(entity);
            if (newRecipeEntity.is_private == null)
                newRecipeEntity.is_private = false;

            if (newRecipeEntity.author == null || newRecipeEntity.author == "uid")
                newRecipeEntity.author = "13b865f7-d6a6-4204-a349-7f379b232f0c";

            var ingredients = entity.ingredients;
            //create list ingre
            List<IngredientEntity> listEngredient = new List<IngredientEntity>();
            foreach (var ingredient in ingredients)
            {
                if(ingredient.id == null)
                {
                    if (!_ingredientRepo.IngredientValid(ingredient.name))
                    {
                        IngredientEntity newIngre = new IngredientEntity
                        {
                            name = ingredient.name,
                            isLiquid = ingredient.isLiquid,
                            ratio = 1,
                            amount = ingredient.amount,

                        };
                        await _ingredientRepo.InsertIngredient(newIngre, true);
                    }
                    var ingreItem = await _ingredientRepo.GetIngredientByName(ingredient.name);
                    ingreItem.amount = ingredient.amount;
                    listEngredient.Add(ingreItem);
                }
                else
                {
                    var ingreItem = await _ingredientRepo.GetIngredientById(ingredient.id ?? 1);
                    ingreItem.amount = ingredient.amount;
                    listEngredient.Add(ingreItem);
                }
            }
            //create recipe
            var newRecipe = await _recipeResposity.InsertAsync(newRecipeEntity);

            ////update recipe_ingredient with ingredient and nutri
            await _recipeResposity.InsertRecipeIngredient(newRecipe, listEngredient);
            newRecipe.ingredients = listEngredient;

            ////add direction 
            await _recipeResposity.InsertDirection(newRecipe, entity.directions);

            ////update nutrition for recipe
            await _recipeResposity.UpdateNutrition(newRecipe, listEngredient);

            return newRecipe;
        }
        public Task<List<RecipeEntity?>> GetAll()
        {
            throw new NotImplementedException();
        }
        public async Task<List<RecipeEntity>> Search(RecipeSearchReq option)
        {
            return await _recipeSearchRepo.Search(option);
        }
        public async Task<List<RecipeEntity>> AddFromExelAsync(IFormFile file)
        {
            try
            {
                //parse
                List<RecipeReq> listRecipeDto = ParseRecipeFromExcel(file);

                List<RecipeEntity> listRecipe = new List<RecipeEntity>();
                #region Add
                foreach (var item in listRecipeDto)
                {
                    await Add(item);
                }
                #endregion
                return listRecipe;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<RecipeEntity>();
            }
        }
        static List<RecipeReq> ParseRecipeFromExcel(IFormFile file)
        {
            #region parse excel -> list recipe
            List<RecipeReq> listRecipeDto = new List<RecipeReq>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            {
                var package = new ExcelPackage(file.OpenReadStream());
                var worksheet = package.Workbook.Worksheets[1];
                var rowCount = worksheet.Dimension.Rows;
                for (int row = 50; row <= rowCount + 1; row++)
                {
                    #region validate each row. if fail -> continue

                    //rating
                    float temp;
                    if (worksheet.Cells[row, 4].Value?.ToString() == null) continue;

                    //time
                    if (worksheet.Cells[row, 6].Value?.ToString() == null) continue;
                    //if (worksheet.Cells[row, 7].Value?.ToString() == null) continue;
                    if (!IsValidTimeFormat(worksheet.Cells[row, 6].Value?.ToString())) continue;
                    //if (!IsValidTimeFormat(worksheet.Cells[row, 7].Value?.ToString())) continue;

                    //serving size > 0
                    if (worksheet.Cells[row, 8].Value?.ToString() == null) continue;
                    if (int.Parse(worksheet.Cells[row, 8].Value?.ToString()) <= 0) continue;

                    //private
                    bool prv = false;
                    Boolean.TryParse(worksheet.Cells[row, 11].Value?.ToString(), out prv);

                    //
                    #endregion


                    #region new recipe
                    RecipeReq entity = new RecipeReq();

                    entity.name = worksheet.Cells[row, 3].Value?.ToString();
                    entity.rating = float.Parse(worksheet.Cells[row, 4].Value.ToString());
                    entity.image = worksheet.Cells[row, 5].Value?.ToString();
                    entity.totalTime = worksheet.Cells[row, 6].Value.ToString();
                    entity.active_time = worksheet.Cells[row, 7].Value?.ToString();
                    entity.serving_size = int.Parse(worksheet.Cells[row, 8].Value?.ToString());
                    entity.introduction = worksheet.Cells[row, 9].Value?.ToString();
                    entity.author_note = worksheet.Cells[row, 10].Value?.ToString();
                    entity.is_private = prv;// parsed in validate
                    entity.ingredients = ParseIngredients(worksheet.Cells[row, 12].Value.ToString());
                    entity.directions = ParseDirection(worksheet.Cells[row, 14].Value.ToString());
                    entity.author = "13b865f7-d6a6-4204-a349-7f379b232f0c";
                    #endregion

                    listRecipeDto.Add(entity);
                }
                return listRecipeDto;
            }
            #endregion
        }
        static List<Recipe_IngredientReq> ParseIngredients(string input)
        {
            string[] rawIngredients = input.Split('|', StringSplitOptions.RemoveEmptyEntries);
            List<Recipe_IngredientReq> ingredients = new List<Recipe_IngredientReq>();

            foreach (var rawIngredient in rawIngredients)
            {
                // Sử dụng biểu thức chính quy để tìm tên và lượng từ chuỗi
                Match match = Regex.Match(rawIngredient.Trim(), @"([^\d]+)\s*(\d+)(g|ml)");

                if (match.Success)
                {
                    string name = match.Groups[1].Value.Trim();
                    decimal amount = decimal.Parse(match.Groups[2].Value);
                    bool isLiquid = match.Groups[3].Value == "g";

                    ingredients.Add(new Recipe_IngredientReq
                    {
                        name = name,
                        amount = amount,
                        isLiquid = isLiquid
                    });
                }
            }

            return ingredients;
        }
        static bool IsValidTimeFormat(string input)
        {
            Regex regex = new Regex(@"^(?:(\d+)h)?(?:(\d+)m)?(?:(\d+)s)?$");
            Match match = regex.Match(input);

            if (match.Success)
            {
                int hours = match.Groups[1].Success ? int.Parse(match.Groups[1].Value) : 0;
                int minutes = match.Groups[2].Success ? int.Parse(match.Groups[2].Value) : 0;
                int seconds = match.Groups[3].Success ? int.Parse(match.Groups[3].Value) : 0;
                return true;
            }

            return false;
        }
        static List<RecipeDirectionReq> ParseDirection(string input)
        {
            List<RecipeDirectionReq> parsedData = new List<RecipeDirectionReq>();
            string[] items = input.Split('|');

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = items[i].Trim();
                int textEndIndex = items[i].LastIndexOf("Huong-Dan/");
                string text = items[i].Substring(0, textEndIndex).Trim();
                string path = items[i].Substring(textEndIndex).Trim();

                parsedData.Add(new RecipeDirectionReq
                {
                    step = i + 1,
                    direction = text,
                    image = path,
                });
            }

            return parsedData;
        }
        public async Task<RecipeRes> RecipeDetail(int id)
        {
            var recipeEntity = await _recipeResposity.FindByIdAsync(id);
            if (recipeEntity != null)
            {
                RecipeRes recipeRes = new RecipeRes();
                //bind
                recipeRes.name = recipeEntity.name;
                recipeRes.rating = recipeEntity.rating;
                recipeRes.totalTime = recipeEntity.totalTime.ToString("s") + "Z";
                recipeRes.serving_size = recipeEntity.serving_size;
                recipeRes.introduction = recipeEntity.introduction;
                recipeRes.author_note = recipeEntity.author_note;
                recipeRes.image = recipeEntity.image;
                recipeRes.createAt = recipeEntity.createdAt;

                //find author
                var authorEntity = await _authorRepo.FindByIdAsync(recipeEntity.author);
                recipeRes.author = new AuthorRes();
                recipeRes.author.uid = authorEntity.uid;
                recipeRes.author.name = authorEntity.name;
                recipeRes.author.avatar = authorEntity.avatar;
                recipeRes.author.introduction = authorEntity.introduction;

                //find ingredient
                var ingredientRes = _ingredientRepo.GetIngredientsByRecipeId(recipeEntity.id);
                recipeRes.ingredients = ingredientRes;

                //find nutrtion
                var nutritionEntity = await _nutritionRepo.FindByIdAsync(recipeEntity.nutrition_info_id);


                recipeRes.nutrition_info = nutritionEntity;


                //find direction
                var direction = _directionRepo.GetDirectionByRecipeId(recipeEntity.id);
                recipeRes.directions = direction;

                

                //find comment
                var comment = _commentRepo.GetCommentByRecipeId(recipeEntity.id);
                recipeRes.comments = comment;

                //find Related Recipe
                var relatedRecipes = _recipeResposity.GetRelatedRecipeByAuthor(recipeEntity.author);
                foreach(var related in relatedRecipes)
                {
                    related.author = recipeRes.author;
                }
                recipeRes.relatedRecipes = relatedRecipes;

                return recipeRes;
            }
            else
                return null;
        }

        public async Task<List<RecipeRes>> GetAllRecipe(PageReq page)
        {
            var test = await RecipeDetail(2);
            //List<int> recipeIds = _recipeResposity.GetAllRecipeId(page);
            List<RecipeRes> result = new List<RecipeRes>();
            
            //foreach (var recipdId in recipeIds)
            //{
            //    var recipe = await RecipeDetail(1);
            //    result.Add(recipe);
            //}
            return result;
        }
    }
}
