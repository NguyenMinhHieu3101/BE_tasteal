using BE_tasteal.API.AppSettings;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Persistence.Repository.AuthorRepo;
using BE_tasteal.Persistence.Repository.PlanRepo;
using BE_tasteal.Persistence.Repository.RecipeRepo;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class PlanController : Controller
    {
        private readonly IPlanRepo _planRepo;
        private readonly IUserRepo _userRepo;
        private readonly IRecipeRepository _recipeRepository;

        public PlanController(IPlanRepo planRepo,
            IUserRepo userRepo,
            IRecipeRepository recipeRepository)
        {
            _planRepo = planRepo;
            _userRepo = userRepo;
            _recipeRepository = recipeRepository;
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> AllCart(string accountId)
        {
            try
            {
                if (await _userRepo.FindByIdAsync(accountId) == null)
                    return BadRequest("User Id invalid");

                var allCart = _planRepo.getListPlanItem(accountId);

                return Ok(allCart);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("addorupdate")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> addrecipe(PlanReq req)
        {
            try
            {
                if (await _userRepo.FindByIdAsync(req.account_id) == null)
                    return BadRequest("User Id invalid");

                foreach (var item in req.recipeIds)
                {
                    if (await _recipeRepository.FindByIdAsync(item) == null)
                        return BadRequest("recipe id invalid");
                }

                var result = await _planRepo.addRecipeToPlan(req);

                if (!result)
                    return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpDelete]
        [Route("")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> deletePlanItem(PlanDeleteReq req)
        {
            try
            {
                if (await _userRepo.FindByIdAsync(req.account_id) == null)
                    return BadRequest("User Id invalid");

                if (await _recipeRepository.FindByIdAsync(req.recipeId) == null)
                    return BadRequest("recipe Id invalid");

                var result = await _planRepo.deleteRecipePlan(req);

                if (!result)
                    return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
