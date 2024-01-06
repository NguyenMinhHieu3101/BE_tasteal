using BE_tasteal.Business.IngredientType;
using BE_tasteal.Business.PantryItem;
using BE_tasteal.Entity.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class PantryItemController :Controller
    {
        private readonly IPantryItemBusiness _iPantryItemBusiness;

        public PantryItemController(IPantryItemBusiness iPantryItemBusiness)
        {
            _iPantryItemBusiness = iPantryItemBusiness;
        }
        [HttpPost]
        [Route("pantry_item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddPantryItem(PantryItemReq item)
        {
            try
            {
                var all = await _iPantryItemBusiness.addPantryItem(item);
                return Ok(all);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("pantry_item/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemovePantryItem(int id)
        {
            try
            {
                var all = await _iPantryItemBusiness.removePantryItem(id);
                return Ok(all);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
