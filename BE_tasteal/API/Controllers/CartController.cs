using BE_tasteal.Business.Cart;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class CartController : Controller
    {
        private readonly ICartBusiness _cartBusiness;

        public CartController(ICartBusiness cartBusiness)
        {
            _cartBusiness = cartBusiness;
        }
        [HttpPost]
        [Route("allcart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        public IActionResult UpdateServingSize(int CardId, int servingSize)
        {
            try
            {
                return Ok(_cartBusiness.UpdateServingSize(CardId, servingSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCart(int cartId)
        {
            try
            {
                return Ok(_cartBusiness.DeleteCart(cartId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut()]
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
                return BadRequest(ex.Message);
            }
        }
    }
}
