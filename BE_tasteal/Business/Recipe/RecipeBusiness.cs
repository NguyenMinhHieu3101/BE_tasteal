using AutoMapper;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Interface.IngredientRepo;
using BE_tasteal.Persistence.Interface.RecipeRepo;
using System.Text.RegularExpressions;

namespace BE_tasteal.Business.Recipe
{
    public class RecipeBusiness : IRecipeBusiness<RecipeDto, RecipeEntity>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SanPhamBusiness> _logger;
        private readonly IRecipeRepository _recipeResposity;
        private readonly IRecipeSearchRepo _recipeSearchRepo;
        private readonly IIngredientRepo _ingredientRepo;
        public RecipeBusiness(IMapper mapper,
           ILogger<SanPhamBusiness> logger,
           IRecipeRepository recipeResposity,
           IRecipeSearchRepo recipeSearchRepo,
           IIngredientRepo ingredientRepo)
        {
            _mapper = mapper;
            _logger = logger;
            _recipeResposity = recipeResposity;
            _recipeSearchRepo = recipeSearchRepo;
            _ingredientRepo = ingredientRepo;
        }
        public async Task<RecipeEntity?> Add(RecipeDto entity)
        {
            var newRecipeEntity = _mapper.Map<RecipeEntity>(entity);
            if (newRecipeEntity.is_private == null)
                newRecipeEntity.is_private = false;

            if (newRecipeEntity.author == null || newRecipeEntity.author == 0)
                newRecipeEntity.author = 1;

            var ingredients = entity.ingredients;
            //create list ingre
            List<IngredientEntity> listEngredient = new List<IngredientEntity>();
            foreach (var ingredient in ingredients)
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
                listEngredient.Add(await _ingredientRepo.GetIngredientByName(ingredient.name));
            }

            //create recipe
            var newRecipe = await _recipeResposity.InsertAsync(newRecipeEntity);

            ////update recipe_ingredient with ingredient and nutri
            //await _recipeResposity.InsertRecipeIngredient(newRecipe, listEngredient);

            ////add direction 
            //await _recipeResposity.InsertDirection(newRecipe, entity.direction);

            ////update nutrition for recipe
            //await _recipeResposity.UpdateNutrition(newRecipe, listEngredient);

            //_logger.LogInformation($"Added new recipe ", entity);
            return new RecipeEntity();
        }

        public Task<List<RecipeEntity?>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<RecipeEntity>> Search(RecipeSearchDto option)
        {
            return await _recipeSearchRepo.Search(option);
        }

        public Task<List<RecipeEntity>?> Search()
        {
            throw new NotImplementedException();
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

        static List<(string Description, string ImageLink)> ParseInputString(string input)
        {
            List<(string Description, string ImageLink)> parsedData = new List<(string, string)>();
            string[] nodes = input.Split('|');

            foreach (var node in nodes)
            {
                string[] parts = node.Trim().Split(' ');

                string description = "";
                string imageLink = null;

                // Tìm liên kết hình ảnh nếu có
                foreach (var part in parts)
                {
                    if (Uri.IsWellFormedUriString(part, UriKind.Absolute))
                    {
                        imageLink = part;
                        break;
                    }
                }

                // Nếu không tìm thấy liên kết hình ảnh, toàn bộ node là mô tả (Description)
                if (imageLink == null)
                {
                    description = string.Join(" ", parts).Trim();
                }
                else
                {
                    // Nếu có liên kết hình ảnh, các phần khác của node trước liên kết đó là mô tả (Description)
                    description = string.Join(" ", parts, 0, Array.IndexOf(parts, imageLink)).Trim();
                }

                parsedData.Add((description, imageLink));
            }

            return parsedData;
        }
    }
}