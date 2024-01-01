using BE_tasteal.Business.Cart;
using BE_tasteal.Business.PantryItem;
using BE_tasteal.Entity.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class CartItemController :Controller
    {
        private readonly ICartItemBusiness _cartItemBusiness;

        public CartItemController(ICartItemBusiness cartItemBusiness)
        {
            _cartItemBusiness = cartItemBusiness;
        }
        [HttpPost]
        [Route("add-recipe-cart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddRecipeToCart(RecipeToCartReq req)
        {
            try
            {
                var allCart = await _cartItemBusiness.addRecipeToCart(req);
                return Ok(allCart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
