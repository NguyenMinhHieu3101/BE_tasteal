using BE_tasteal.Business.RatingBusiness;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Persistence.Repository.AuthorRepo;
using BE_tasteal.Persistence.Repository.RecipeRepo;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("2.0")]
    public class RatingController : Controller
    {
        private readonly IRatingBusiness _ratingBusiness;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserRepo _userRepo;
        public RatingController(
            IRatingBusiness commentBusiness,
            IRecipeRepository recipeRepository,
            IUserRepo userRepo)
        {
            _ratingBusiness = commentBusiness;
            _recipeRepository = recipeRepository;
            _userRepo = userRepo;
        }

        [HttpGet]
        [Route("Recipe/{recipe_id}/Rating")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getRecipeComments(int recipe_id)
        {

            try
            {
                if (await _recipeRepository.FindByIdAsync(recipe_id) == null)
                {
                    return BadRequest("Recipe id invalid");
                }
                var ratingTotal = await _recipeRepository.FindByIdAsync(recipe_id);
                var result = new
                {
                    recipe_id = recipe_id,
                    rating = ratingTotal.rating,
                    comments = await _ratingBusiness.GetCommentByRecipeId(recipe_id)
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpPost]
        [Route("Recipe/{recipe_id}/Rating")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getRecipeComments(
             int recipe_id,
            [FromBody] RatingReq req)
        {

            try
            {
                if (req.rating < 0 || req.rating > 5)
                {
                    return BadRequest("0<= rating <=5");
                }
                if (await _recipeRepository.FindByIdAsync(recipe_id) == null)
                {
                    return BadRequest("Recipe id invalid");
                }
                if (await _userRepo.FindByIdAsync(req.account_id) == null)
                {
                    return BadRequest("User id invalid");
                }
                var result = await _ratingBusiness.InsertCommentAsync(recipe_id, req);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPut]
        [Route("Recipe/{recipe_id}/Rating/{id}")]
        public async Task<IActionResult> putRecipeComment(
             int recipe_id, int id,
            [FromBody] RatingPutReq req)
        {

            try
            {
                if (req.rating < 0 || req.rating > 5)
                {
                    return BadRequest("0<= rating <=5");
                }
                if (await _recipeRepository.FindByIdAsync(recipe_id) == null)
                {
                    return BadRequest("Recipe id invalid");
                }
                if (await _ratingBusiness.FindAsync(id) == null)
                {
                    return BadRequest("Rating id invalid");
                }
                var result = await _ratingBusiness.UpdateCommentAsync(recipe_id, id, req.rating);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpDelete]
        [Route("Recipe/{recipe_id}/Rating/{id}")]
        public async Task<IActionResult> deleteRecipeComment(
             int recipe_id, int id)
        {

            try
            {
                if (await _recipeRepository.FindByIdAsync(recipe_id) == null)
                {
                    return BadRequest("Recipe id invalid");
                }
                var comment = await _ratingBusiness.FindAsync(id);
                if (comment == null)
                {
                    return BadRequest("Comment id invalid");
                }
                if (comment.recipe_id != recipe_id)
                {
                    return BadRequest("Rating does not belong to recipe");
                }

                var result = await _ratingBusiness.DeleteCommentAsync(recipe_id, id);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
                }
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
