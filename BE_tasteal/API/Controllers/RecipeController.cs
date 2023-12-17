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
        private readonly IRecipeBusiness<RecipeReq, RecipeEntity> _recipeBusiness;
        public RecipeController(
            IRecipeBusiness<RecipeReq, RecipeEntity> recipeBusiness)
        {
            _recipeBusiness = recipeBusiness;
        }
        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RecipeEntity))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddRecipe([FromBody] RecipeReq _recipe)
        {
            return Created(string.Empty, await _recipeBusiness.Add(_recipe));
        }
        [HttpPost]
        [Route("Search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> SearchRecipe([FromBody] RecipeSearchReq option)
        {
            try
            {
                var recipe = await _recipeBusiness.Search(option);
                return Ok(recipe);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
        [HttpPost]
        [Route("getall")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RecipeEntity>))]
        public async Task<IActionResult> GetAllRecipe([FromBody] PageReq page)
        {
            try
            {
                var recipe = await _recipeBusiness.GetAllRecipe(page);
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
        public async Task<IActionResult> GetRecipes(int id)
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
        [HttpPost]
        [Route("GetRecipes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRecipe(List<int> id)
        {
            try
            {
                var recipes = await _recipeBusiness.GetRecipes(id);
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("keywords")]
        public async Task<IActionResult> GetKeyWord()
        {
            try
            {
                var recipes = await _recipeBusiness.GetKeyWords();
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("recipe")]
        public async Task<IActionResult> DeleteRecipe(int recipeId)
        {
            try
            {
                var recipes = await _recipeBusiness.DeleteRecipe(recipeId);
                return Ok(recipes == 1 ? "success" :"fail");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("recipeByUserId")]
        public async Task<IActionResult> getRecipeByUserId(
            [FromQuery] string userId,
            [FromBody] PageReq page)
        {
            try
            {
                var recipes = _recipeBusiness.getRecipeByUserId(userId, page);
                if(recipes == null)
                {
                    return BadRequest("User invalid");
                }
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
