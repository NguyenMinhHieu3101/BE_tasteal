using BE_tasteal.Business.Cart;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Persistence.Repository.CookBookRepo;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class CookBookController : Controller
    {
        private readonly CookBookRepo _cookBookRepo;
        public CookBookController(CookBookRepo cartBusiness)
        {
            _cookBookRepo = cartBusiness;
        }

        [HttpGet]
        [Route("cookbook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllCookbookByUid(string uid)
        {
            try
            {
                var allCart = await _cookBookRepo.getAllCookBookByUid(uid);
                return Ok(allCart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("cookbook-recipe")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCookBookRecipeById(string id)
        {
            try
            {
                var allCart = await _cookBookRepo.DeleteCookBookRecipeById(id);
                if(allCart > 0)
                {
                    return Ok("success");
                }
                else
                    return BadRequest(false);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("newRecipeCookBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MoveRecipeToNewCookBook(NewRecipeCookBook id)
        {
            try
            {
                var allCart = await _cookBookRepo.MoveRecipeToNewCookBook(id);
                if (allCart > 0)
                    return Ok(true);
                else
                    return BadRequest(false);
            }
            catch (Exception ex)
            {
                return BadRequest(false);
            }
        }
    }
}
