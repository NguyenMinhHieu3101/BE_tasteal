using BE_tasteal.Business.Cart;
using BE_tasteal.Business.User;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class UserController : Controller
    {
        private readonly IUserBusiness _userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }
        [HttpPost]
        [Route("signup")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> signup(AccountReq accountReq)
        {
            try
            {
                var entity = await _userBusiness.signup(accountReq);
                return Created(string.Empty, entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return UnprocessableEntity();
            }
        }
        [HttpPost]
        [Route("updateuser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> updateuser(AccountReq accountReq)
        {
            try
            {
                var entity = await _userBusiness.udpateAccount(accountReq);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest("update fail");
            }
        }
    }
}
