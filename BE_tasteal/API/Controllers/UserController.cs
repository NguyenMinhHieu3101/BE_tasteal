using BE_tasteal.Business.Cart;
using BE_tasteal.Business.User;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.AuthorRepo;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class UserController : Controller
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IUserRepo _userRepo;
        public UserController(IUserBusiness userBusiness, IUserRepo userRepo)
        {
            _userBusiness = userBusiness;
            _userRepo = userRepo;
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
        [HttpPut]
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
        [HttpPost]
        [Route("allusers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getAllUsers(PageReq page)
        {
            try
            {
                var account = await _userBusiness.getAllUser(page);
                return Ok(account);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> getUser(string accountId)
        {
            try
            {
               
                var account = await _userBusiness.getUser(accountId);
                if (account == null)
                    return BadRequest("Account invalid");

                return Ok(account);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        [HttpPost]
        [Route("getAccounts")]
        public async Task<IActionResult> getAccountByListUid(List<string> accountId)
        {
            try
            {
                List<AccountEntity> users = new List<AccountEntity>();
                foreach (var item in accountId)
                {
                    var user = await _userRepo.FindByIdAsync(item);
                    if (user == null)
                        return BadRequest("UserId invalid");
                    else
                        users.Add(user);
                }
                return Ok(users);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
