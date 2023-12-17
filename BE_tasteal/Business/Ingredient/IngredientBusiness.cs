using AutoMapper;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.IngredientRepo;
using OfficeOpenXml;

namespace BE_tasteal.Business.Ingredient
{
    public class IngredientBusiness : IIngredientBusiness
    {
        private readonly IMapper _mapper;
        private readonly IIngredientRepo _ingredientRepo;

        public IngredientBusiness(IMapper mapper, IIngredientRepo ingredientRepo)
        {
            _mapper = mapper;
            _ingredientRepo = ingredientRepo;
        }
        public async Task<List<IngredientEntity>> AddFromExelAsync(IFormFile file)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<IngredientEntity> entities = new List<IngredientEntity>();
            using (var package = new ExcelPackage(file.OpenReadStream()))
            {
                var worksheet = package.Workbook.Worksheets["ingredient"];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 3; row <= rowCount + 1; row++)
                {
                    var ingredientName = worksheet.Cells[row, 1].Value?.ToString();
                    if (ingredientName == null)
                        continue;
                    if (_ingredientRepo.IngredientValid(ingredientName))
                        continue;

                    var ingredientType = worksheet.Cells[row, 2].Value?.ToString();
                    if (ingredientType == null)
                        continue;


                    Ingredient_TypeEntity type = await _ingredientRepo.GetIngredientType(ingredientType);

                    #region nutrition info
                    decimal calories;
                    decimal.TryParse(worksheet.Cells[row, 3].Value?.ToString(), out calories);
                    decimal fat;
                    decimal.TryParse(worksheet.Cells[row, 4].Value?.ToString(), out fat);
                    decimal saturated_fat;
                    decimal.TryParse(worksheet.Cells[row, 5].Value?.ToString(), out saturated_fat);
                    decimal trans_fat;
                    decimal.TryParse(worksheet.Cells[row, 6].Value?.ToString(), out trans_fat);
                    decimal cholesterol;
                    decimal.TryParse(worksheet.Cells[row, 7].Value?.ToString(), out cholesterol);
                    decimal carbohydrates;
                    decimal.TryParse(worksheet.Cells[row, 8].Value?.ToString(), out carbohydrates);
                    decimal fiber;
                    decimal.TryParse(worksheet.Cells[row, 9].Value?.ToString(), out fiber);
                    decimal sugars;
                    decimal.TryParse(worksheet.Cells[row, 10].Value?.ToString(), out sugars);
                    decimal protein;
                    decimal.TryParse(worksheet.Cells[row, 11].Value?.ToString(), out protein);
                    decimal sodium;
                    decimal.TryParse(worksheet.Cells[row, 12].Value?.ToString(), out sodium);
                    decimal vitaminD;
                    decimal.TryParse(worksheet.Cells[row, 13].Value?.ToString(), out vitaminD);
                    decimal calcium;
                    decimal.TryParse(worksheet.Cells[row, 14].Value?.ToString(), out calcium);
                    decimal iron;
                    decimal.TryParse(worksheet.Cells[row, 15].Value?.ToString(), out iron);
                    decimal potassium;
                    decimal.TryParse(worksheet.Cells[row, 16].Value?.ToString(), out potassium);
                    #endregion
                    Nutrition_InfoEntity nutrition = new Nutrition_InfoEntity
                    {
                        calories = calories,
                        fat = fat,
                        saturated_fat = saturated_fat,
                        trans_fat = trans_fat,
                        cholesterol = cholesterol,
                        carbohydrates = carbohydrates,
                        fiber = fiber,
                        sugars = sugars,
                        protein = protein,
                        sodium = sodium,
                        vitaminD = vitaminD,
                        calcium = calcium,
                        iron = iron,
                        potassium = potassium,
                    };
                    nutrition = await _ingredientRepo.InsertNutrition(nutrition);

                    //parse Liquid. default false
                    bool isLiquid;
                    if (!Boolean.TryParse(worksheet.Cells[row, 17].Value?.ToString(), out isLiquid))
                    {
                        isLiquid = false;
                    }

                    //parse ratio, default = 0
                    decimal ratio;
                    if (!decimal.TryParse(worksheet.Cells[row, 18].Value?.ToString(), out ratio))
                    {
                        ratio = 0;
                    }

                    string image;
                    if (worksheet.Cells[row, 19].Value != null)
                    {
                        image = worksheet.Cells[row, 19].Value.ToString();
                    }
                    else
                    {
                        image = null;
                    }

                    IngredientEntity newIngredient = new IngredientEntity
                    {
                        name = ingredientName,
                        image = image,
                        nutrition_info_id = nutrition.id,
                        type_id = type.id,
                        isLiquid = isLiquid,
                        ratio = ratio,
                    };

                    var Ingredient = await _ingredientRepo.InsertAsync(newIngredient);
                    entities.Add(Ingredient);
                }
            }
            return entities;
        }


        public async Task<List<IngredientEntity>> GetIngredients()
        {
            return await _ingredientRepo.GetAllIngredient();
        }
    }
}
