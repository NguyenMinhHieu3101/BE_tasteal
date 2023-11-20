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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RecipeEntity))]
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
        //[HttpPost]
        //[Route("GetAllRecipe")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RecipeEntity>))]
        //public IActionResult GetAllRecipe([FromBody] PageReq page)
        //{
        //    try
        //    {
        //        var recipe = _recipeBusiness.GetAllRecipe(page);
        //        return Ok(recipe);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.ToString());
        //    }
        //}
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
    }
}
