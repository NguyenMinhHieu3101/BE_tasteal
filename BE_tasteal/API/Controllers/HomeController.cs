using BE_tasteal.API.AppSettings;
using BE_tasteal.Business.HomeBusiness;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class HomeController : Controller
    {
        private readonly IHomeBusiness _homeBusiness;

        public HomeController(IHomeBusiness homeBusiness)
        {
            _homeBusiness = homeBusiness;
        }
        [HttpGet]
        [Route("getoccasion")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OccasionEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetOccasion()
        {
            try
            {
                var occasions = _homeBusiness.GetAllOccasion();
                return Ok(occasions);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("recipebydatetime")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OccasionEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult RecipeByDateTime(PageFilter filter)
        {
            try
            {
                var occasions = _homeBusiness.GetRecipeByTime(filter);
                return Ok(occasions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("recipebyrating")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OccasionEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult RecipeByRating(PageFilter filter)
        {
            try
            {
                var recipes = _homeBusiness.GetRecipeByRating(filter);
                return Ok(recipes);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }
        [HttpPost]
        [Route("authors")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OccasionEntity))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult GetAuthor(PageFilter filter)
        {
            try
            {
                var occasions = _homeBusiness.GetAuthor(filter);
                return Ok(occasions);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }
    }
}
