using AutoMapper;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Interface.IngredientRepo;
using BE_tasteal.Persistence.Interface.RecipeRepo;
using Newtonsoft.Json;
using OfficeOpenXml;
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
                var ingreItem = await _ingredientRepo.GetIngredientByName(ingredient.name);
                ingreItem.amount = ingredient.amount;
                listEngredient.Add(ingreItem);
            }
            Console.WriteLine(JsonConvert.SerializeObject(listEngredient));
            //create recipe
            var newRecipe = await _recipeResposity.InsertAsync(newRecipeEntity);

            ////update recipe_ingredient with ingredient and nutri
            await _recipeResposity.InsertRecipeIngredient(newRecipe, listEngredient);
            newRecipe.ingredients = listEngredient;

            ////add direction 
            await _recipeResposity.InsertDirection(newRecipe, entity.direction);

            ////update nutrition for recipe
            await _recipeResposity.UpdateNutrition(newRecipe, listEngredient);

            return newRecipe;
        }
        public Task<List<RecipeEntity?>> GetAll()
        {
            throw new NotImplementedException();
        }
        public async Task<List<RecipeEntity>> Search(RecipeSearchDto option)
        {
            return await _recipeSearchRepo.Search(option);
        }
        public async Task<List<RecipeEntity>> AddFromExelAsync(IFormFile file)
        {
            try
            {
                //parse
                List<RecipeDto> listRecipeDto = ParseRecipeFromExcel(file);

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
                _logger.LogError(ex.ToString());
                return new List<RecipeEntity>();
            }
        }
        static List<RecipeDto> ParseRecipeFromExcel(IFormFile file)
        {
            #region parse excel -> list recipe
            List<RecipeDto> listRecipeDto = new List<RecipeDto>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            {
                var package = new ExcelPackage(file.OpenReadStream());
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 3; row <= rowCount + 1; row++)
                {
                    #region validate each row. if fail -> continue

                    //rating
                    float temp;
                    if (worksheet.Cells[row, 2].Value?.ToString() == null) continue;

                    //time
                    if (worksheet.Cells[row, 4].Value?.ToString() == null) continue;
                    if (worksheet.Cells[row, 5].Value?.ToString() == null) continue;
                    if (!IsValidTimeFormat(worksheet.Cells[row, 4].Value?.ToString())) continue;
                    if (!IsValidTimeFormat(worksheet.Cells[row, 5].Value?.ToString())) continue;

                    //serving size > 0
                    if (worksheet.Cells[row, 6].Value?.ToString() == null) continue;
                    if (int.Parse(worksheet.Cells[row, 6].Value?.ToString()) <= 0) continue;

                    //private
                    bool prv = false;
                    Boolean.TryParse(worksheet.Cells[row, 9].Value?.ToString(), out prv);

                    //
                    #endregion


                    #region new recipe
                    RecipeDto entity = new RecipeDto();


                    entity.name = worksheet.Cells[row, 1].Value?.ToString();
                    entity.rating = float.Parse(worksheet.Cells[row, 2].Value.ToString());
                    entity.image = worksheet.Cells[row, 3].Value?.ToString();
                    entity.totalTime = worksheet.Cells[row, 4].Value.ToString();
                    entity.active_time = worksheet.Cells[row, 5].Value.ToString();
                    entity.serving_size = int.Parse(worksheet.Cells[row, 6].Value?.ToString());
                    entity.introduction = worksheet.Cells[row, 7].Value?.ToString();
                    entity.author_note = worksheet.Cells[row, 8].Value.ToString();
                    entity.is_private = prv;// parsed in validate
                    entity.ingredients = ParseIngredients(worksheet.Cells[row, 10].Value.ToString());
                    entity.direction = ParseDirection(worksheet.Cells[row, 12].Value.ToString());
                    entity.author = 1;
                    #endregion

                    listRecipeDto.Add(entity);
                }
                return listRecipeDto;
            }
            #endregion
        }
        public List<RecipeEntity> GetRecipeEntities()
        {
            var list = _recipeResposity.GetRecipesWithIngredientsAndNutrition();
            return list;
        }
        static List<Recipe_IngredientDto> ParseIngredients(string input)
        {
            string[] rawIngredients = input.Split('|', StringSplitOptions.RemoveEmptyEntries);
            List<Recipe_IngredientDto> ingredients = new List<Recipe_IngredientDto>();

            foreach (var rawIngredient in rawIngredients)
            {
                // Sử dụng biểu thức chính quy để tìm tên và lượng từ chuỗi
                Match match = Regex.Match(rawIngredient.Trim(), @"([^\d]+)\s*(\d+)(g|ml)");

                if (match.Success)
                {
                    string name = match.Groups[1].Value.Trim();
                    decimal amount = decimal.Parse(match.Groups[2].Value);
                    bool isLiquid = match.Groups[3].Value == "g";

                    ingredients.Add(new Recipe_IngredientDto
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
        static List<RecipeDirectionDto> ParseDirection(string input)
        {
            List<RecipeDirectionDto> parsedData = new List<RecipeDirectionDto>();
            string[] nodes = input.Split('|');

            for (int i = 0; i < nodes.Length; i++)
            {
                string[] parts = nodes[i].Trim().Split(' ');

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

                parsedData.Add(new RecipeDirectionDto
                {
                    step = i,
                    direction = description,
                    image = imageLink,
                });
            }

            return parsedData;
        }
    }
}
