using BE_tasteal.Business.IngredientType;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
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
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetIngredientTypeByID(int id)
        {
            try
            {
                DAGIngredientTypeReq item = new DAGIngredientTypeReq { id = id };
                var all = await _ingredientTypeBusiness.GetIngredientTypeById(item);

                if (all == null)
                    return BadRequest("id invalid");
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
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteIngredientType(int id)
        {
            try
            {
                DAGIngredientTypeReq item = new DAGIngredientTypeReq { id = id };
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
