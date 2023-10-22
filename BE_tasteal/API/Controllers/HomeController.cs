using BE_tasteal.API.AppSettings;
using BE_tasteal.Business.HomeBusiness;
using BE_tasteal.Entity.DTO;
using BE_tasteal.Entity.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IHomeBusiness _homeBusiness;

        public HomeController(IHomeBusiness homeBusiness, ILogger logger)
        {
            _homeBusiness = homeBusiness;
            _logger = logger;
        }
        [HttpPost]
        [Route("getoccasion")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OccasionEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOccasion()
        {
            try
            {
                var occasions = await _homeBusiness.GetAllOccasion();
                return Ok(occasions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("recipebydatetime")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OccasionEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RecipeByDateTime(PageFilter filter)
        {
            try
            {
                var occasions = await _homeBusiness.GetRecipeByTime();
                return Ok(occasions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("recipebyrating")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OccasionEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RecipeByRating(PageFilter filter)
        {
            try
            {
                var occasions = await _homeBusiness.GetRecipeByRating();
                return Ok(occasions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("authors")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OccasionEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetAuthor(PageFilter filter)
        {
            try
            {
                var occasions = await _homeBusiness.GetAuthor();
                return Ok(occasions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
    }
}
