using BE_tasteal.Business.Cart;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.CartRepo;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class CartController : Controller
    {
        private readonly ICartBusiness _cartBusiness;
        private readonly ICartRepo _cartRepo;

        public CartController(
            ICartBusiness cartBusiness,
            ICartRepo cartRepo)
        {
            _cartBusiness = cartBusiness;
            _cartRepo = cartRepo;
        }
        [HttpPost]
        [Route("allcart")]
        public IActionResult AllCart(string accountId)
        {
            try
            {
                var allCart = _cartBusiness.GetCartByAccountId(accountId);
                return Ok(allCart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("servingsize")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateServingSize(int cartId, int servingSize)
        {
            try
            {
                if (await _cartRepo.FindByIdAsync(cartId) == null)
                    return BadRequest("cartId invalid");
                return Ok(_cartBusiness.UpdateServingSize(cartId, servingSize));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpPost]
        [Route("cartitem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult getCartItemByCartId(List<int> cartIds)
        {
            try
            {

                return Ok(_cartBusiness.GetItemByCartId(cartIds));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("cart")]
        public async Task<ActionResult> DeleteCart(int cartId)
        {
            try
            {
                if (await _cartRepo.FindByIdAsync(cartId) == null)
                    return BadRequest("cartId invalid");
                return Ok(_cartBusiness.DeleteCart(cartId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");

            }
        }
        [HttpDelete]
        [Route("allcart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleleCartByAccountId(string accountId)
        {
            try
            {
                return Ok(_cartBusiness.DeleleCartByAccountId(accountId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("cartitemstatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult updateCartItemStatus(int cartID, int ingredientId, bool isBought)
        {
            try
            {
                return Ok(_cartBusiness.UpdateBoughtItem(cartID, ingredientId, isBought));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("personalcarts")]
        public async Task<ActionResult<PersonalCartItemEntity>> GetPersonalCartItem(string userId)
        {
            try
            {
                var result = _cartBusiness.GetPersonalCartItemsWithIngredients(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("personalcart")]
        public async Task<ActionResult<PersonalCartItemEntity>> PostPersonalCartItem(PersonalCartItemReq request)
        {
            try
            {
                var result = await _cartBusiness.PostPersonalCartItem(request);
                if (!result)
                    return BadRequest("IngredientId or UserId invalid");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("personalcart")]
        public async Task<IActionResult> PutPersonalCartItem(PersonalCartItemUpdateReq request)
        {
            try
            {
                var result = await _cartBusiness.PutPersonalCartItem(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");

            }
        }
    }
}
