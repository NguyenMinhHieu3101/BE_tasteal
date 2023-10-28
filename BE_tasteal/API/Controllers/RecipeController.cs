using BE_tasteal.API.AppSettings;
using BE_tasteal.Business.Recipe;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class RecipeController : Controller
    {
        private readonly IRecipeBusiness<RecipeDto, RecipeEntity> _recipeBusiness;
        public RecipeController(
            IRecipeBusiness<RecipeDto, RecipeEntity> recipeBusiness)
        {
            _recipeBusiness = recipeBusiness;
        }
        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RecipeEntity))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddRecipe([FromBody] RecipeDto _recipe)
        {
            return Created(string.Empty, await _recipeBusiness.Add(_recipe));
        }
        [HttpPost]
        [Route("Search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RecipeEntity))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> SearchRecipe([FromBody] RecipeSearchDto option)
        {
            try
            {
                var recipe = await _recipeBusiness.Search(option);
                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return NotFound(ex.ToString());
            }
        }
        [HttpPost]
        [Route("AddFromExcel")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<IngredientEntity>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddRecipeExcel(IFormFile file)
        {
            try
            {
                var recipes = await _recipeBusiness.AddFromExelAsync(file);
                return Ok(JsonConvert.SerializeObject(recipes));
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.ToString());
            }
        }
        [HttpGet]
        [Route("GetAllRecipe")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RecipeEntity>))]
        public IActionResult GetAllRecipe()
        {
            try
            {
                var recipe = _recipeBusiness.GetAllRecipe();
                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("GetRecipe")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RecipeEntity>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRecipe(int id)
        {
            try
            {
                var recipes = await _recipeBusiness.RecipeDetail(id);
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
