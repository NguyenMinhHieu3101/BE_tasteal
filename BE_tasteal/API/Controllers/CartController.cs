using BE_tasteal.Business.Cart;
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
        public IActionResult AllCart(int accountId)
        {
            try
            {
                //var allCart = await _cartBusiness
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
