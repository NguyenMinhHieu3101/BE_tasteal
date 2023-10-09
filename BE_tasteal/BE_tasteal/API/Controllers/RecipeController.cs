using BE_tasteal.API.AppSettings;
using BE_tasteal.Business.Interface;
using BE_tasteal.Entity.DTO;
using BE_tasteal.Entity.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiVersion("1.0")]
    public class RecipeController : Controller
    {
        private readonly IBusiness<RecipeDto, RecipeEntity> _recipeBusiness;
        public RecipeController(
            IBusiness<RecipeDto, RecipeEntity> recipeBusiness)
        {
            _recipeBusiness = recipeBusiness;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RecipeEntity))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddRecipe([FromBody] RecipeDto _recipe)
        {
            RecipeDto re = new RecipeDto();

            re.name = "ád";
            re.rating = 1;
            re.totalTime = new TimeSpan(50);
            re.active_time = new TimeSpan(50);
            re.serving_size = 5;
            re.introduction = "ádkjasd";
            re.author_note = "";
            re.is_private = true;
            re.image = "";
            re.author = null;
            re.nutrition_info_id = null;

            return Created(string.Empty, await _recipeBusiness.Add(re));
        }
    }
}
