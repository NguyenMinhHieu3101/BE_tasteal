using BE_tasteal.Business.Pantry;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Persistence.Repository.IngredientRepo;
using BE_tasteal.Persistence.Repository.Pantry;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class PantryController : Controller
    {
        private readonly PantryBusiness _pantryBusiness;
        private readonly IIngredientRepo _ingredientRepo;
        private readonly IPantryRepo _pantryRepo;
        public PantryController(PantryBusiness pantryBusiness,
            IIngredientRepo ingredientRepo,
            IPantryRepo pantryRepo)
        {
            _pantryBusiness = pantryBusiness;
            _ingredientRepo = ingredientRepo;
            _pantryRepo = pantryRepo;
        }

        [HttpPost]
        [Route("getRecipesByIngredientsAny")]
        public async Task<IActionResult> FindGroupIndexContainingAnyValue(
            [FromBody] RecipesIngreAny req
            )
        {
            try
            {
                foreach (var item in req.ingredients)
                {
                    if (await _ingredientRepo.FindByIdAsync(item) == null)
                        return BadRequest("ingredient id invalid");
                }
                var all = _pantryBusiness.FindGroupIndexContainingAnyValue(req);
                return Ok(all);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpPost]
        [Route("getRecipesByIngredientsAll")]
        public async Task<IActionResult> FindGroupIndexContainingAllValues(
            [FromBody] RecipesIngreAny req
            )
        {
            try
            {
                foreach (var item in req.ingredients)
                {
                    if (await _ingredientRepo.FindByIdAsync(item) == null)
                        return BadRequest("ingredient id invalid");
                }
                var all = _pantryBusiness.FindGroupIndexContainingAllValues(req);
                return Ok(all);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpPost]
        [Route("getRecipesByPantryIdAny")]
        public async Task<IActionResult> FindGroupIndexContainingAnyValuesPantry(
           [FromBody] RecipesPantryAny req
           )
        {
            try
            {
                if (await _pantryRepo.FindByIdAsync(req.pantry_id) == null)
                    return BadRequest($"Not found req.pantry_id");
                var all = _pantryBusiness.FindGroupIndexContainingAnyValuesPantry(req);
                return Ok(all);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpPost]
        [Route("getRecipesByPantryIdAll")]
        public async Task<IActionResult> FindGroupIndexContainingAllValuesPantry(
           [FromBody] RecipesPantryAny req
           )
        {
            try
            {
                if (await _pantryRepo.FindByIdAsync(req.pantry_id) == null)
                    return BadRequest($"Not found req.pantry_id");
                var all = _pantryBusiness.FindGroupIndexContainingAllValuesPantry(req);
                return Ok(all);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
