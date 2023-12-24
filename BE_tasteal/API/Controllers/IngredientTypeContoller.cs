using BE_tasteal.Business.IngredientType;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class IngredientTypeController : Controller
    {
        private readonly IIngredientTypeBusiness _ingredientTypeBusiness;

        public IngredientTypeController(IIngredientTypeBusiness ingredientTypeBusiness)
        {
            _ingredientTypeBusiness = ingredientTypeBusiness;
        }
        [HttpPost]
        [Route("IngredientType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AllIngredientType()
        {
            try
            {
                var all= await _ingredientTypeBusiness.GetAllIngredientType();
                return Ok(all);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
