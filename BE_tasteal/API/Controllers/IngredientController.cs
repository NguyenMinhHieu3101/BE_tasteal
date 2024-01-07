using BE_tasteal.Business.Ingredient;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.IngredientRepo;
using BE_tasteal.Persistence.Repository.IngredientTypeRepo;
using BE_tasteal.Persistence.Repository.NutritionRepo;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class IngredientController : Controller
    {
        private readonly IIngredientBusiness _ingredientBusiness;
        private readonly IIngredientRepo _ingredientRepo;
        private readonly INutritionRepo _nutritionRepo;
        private readonly IIngredientTypeRepo _ingredientTypeRepo;
        public IngredientController(
           IIngredientBusiness ingredientBusiness,
           IIngredientRepo ingredientRepo,
           INutritionRepo nutritionRepo,
           IIngredientTypeRepo ingredientTypeRepo)
        {
            _ingredientBusiness = ingredientBusiness;
            _ingredientRepo = ingredientRepo;
            _nutritionRepo = nutritionRepo;
            _ingredientTypeRepo = ingredientTypeRepo;
        }

        [HttpPost]
        [Route("addfromexcel")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<IngredientEntity>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadExcelFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length <= 0)
                {
                    return BadRequest("Invalid file");
                }
                var ingredients = await _ingredientBusiness.AddFromExelAsync(file);
                return Created(string.Empty, ingredients);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex);
            }

        }

        [HttpPost]
        [Route("getall")]

        public async Task<IActionResult> GetAllIngredient(
            [FromBody] PageReq page)
        {
            try
            {
                var result = await _ingredientBusiness.GetAllIngredient(page);

                if (result.Item1.Count == 0)
                {
                    return NotFound();
                }

                var response = new
                {
                    maxPage = result.Item2,
                    ingredients = result.Item1
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.ToString()}");
                return BadRequest(ex);
            }
        }
        [HttpDelete]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(List<IngredientEntity>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> delete([FromQuery] int id)
        {
            try
            {
                var ingre = await _ingredientRepo.FindByIdAsync(id);
                if (ingre == null)
                {
                    return NotFound("id invalid");
                }

                await _ingredientRepo.DeleteAsync(ingre);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(false);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> get(int id)
        {
            try
            {
                var result = await _ingredientRepo.FindById(id);

                if (result == null)
                    return BadRequest("Id invalid");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(false);
            }
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> postIngredient([FromBody] IngredientPutReq req)
        {
            try
            {
                var type = await _ingredientTypeRepo.FindByIdAsync(req.ingredient_type.id);
                if (type == null)
                    return BadRequest("TypeId invalid");

                Nutrition_InfoEntity nutri_info = new Nutrition_InfoEntity
                {
                    calories = req.nutrition_info.calories,
                    fat = req.nutrition_info.fat,
                    saturated_fat = req.nutrition_info.saturated_fat,
                    trans_fat = req.nutrition_info.trans_fat,
                    cholesterol = req.nutrition_info.cholesterol,
                    carbohydrates = req.nutrition_info.carbohydrates,
                    fiber = req.nutrition_info.fiber,
                    sugars = req.nutrition_info.sugars,
                    protein = req.nutrition_info.protein,
                    sodium = req.nutrition_info.sodium,
                    vitaminD = req.nutrition_info.vitaminD,
                    calcium = req.nutrition_info.calcium,
                    iron = req.nutrition_info.iron,
                    potassium = req.nutrition_info.potassium
                };

                var new_nutr_info = await _nutritionRepo.InsertAsync(nutri_info);

                if (new_nutr_info == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");

                IngredientEntity ingredient = new IngredientEntity
                {
                    name = req.name,
                    image = req.image,
                    nutrition_info_id = new_nutr_info.id,
                    type_id = type.id,
                    isLiquid = req.isLiquid,
                    ratio = req.ratio
                };

                var new_ingredient = await _ingredientRepo.InsertAsync(ingredient);

                if (new_ingredient == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");

                return Ok(new_ingredient);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> putIngredient(int id, [FromBody] IngredientPutReq req)
        {
            try
            {
                var ingredient = await _ingredientRepo.FindByIdAsync(id);

                if (ingredient == null)
                    return BadRequest("IngredientId invalid");


                {
                    var nutri_info = await _nutritionRepo.FindByIdAsync(ingredient.nutrition_info_id);
                    nutri_info.calories = req.nutrition_info.calories;
                    nutri_info.fat = req.nutrition_info.fat;
                    nutri_info.saturated_fat = req.nutrition_info.saturated_fat;
                    nutri_info.trans_fat = req.nutrition_info.trans_fat;
                    nutri_info.cholesterol = req.nutrition_info.cholesterol;
                    nutri_info.carbohydrates = req.nutrition_info.carbohydrates;
                    nutri_info.fiber = req.nutrition_info.fiber;
                    nutri_info.sugars = req.nutrition_info.sugars;
                    nutri_info.protein = req.nutrition_info.protein;
                    nutri_info.sodium = req.nutrition_info.sodium;
                    nutri_info.vitaminD = req.nutrition_info.vitaminD;
                    nutri_info.calcium = req.nutrition_info.calcium;
                    nutri_info.iron = req.nutrition_info.iron;
                    nutri_info.potassium = req.nutrition_info.potassium;

                    await _nutritionRepo.UpdateAsync(nutri_info);
                }

                {
                    ingredient.name = req.name;
                    ingredient.type_id = req.ingredient_type.id;
                    ingredient.image = req.image;
                    ingredient.isLiquid = req.isLiquid;
                    ingredient.ratio = req.ratio;
                }

                await _ingredientRepo.UpdateAsync(ingredient);



                return Ok(ingredient);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
