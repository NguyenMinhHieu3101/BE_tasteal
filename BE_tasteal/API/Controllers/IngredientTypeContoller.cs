using BE_tasteal.Business.IngredientType;
using BE_tasteal.Entity.DTO.Request;
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
        [HttpGet]
        [Route("getall")]
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
        [HttpPost]
        [Route("getbyid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetIngredientTypeByID(DAGIngredientTypeReq item)
        {
            try
            {
                var all = await _ingredientTypeBusiness.GetIngredientTypeById(item);
                return Ok(all);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateIngredientType(CreateIngredientTypeReq item)
        {
            try
            {
                var all = await _ingredientTypeBusiness.CreateIngredientType(item);
                return Ok(all);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateIngredientType(UpdateIngredientTypeReq item)
        {
            try
            {
                var all = await _ingredientTypeBusiness.UpdateIngredientType(item);
                return Ok(all);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteIngredientType(DAGIngredientTypeReq item)
        {
            try
            {
                var all = await _ingredientTypeBusiness.DeleteIngredientType(item);
                return Ok(all);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
