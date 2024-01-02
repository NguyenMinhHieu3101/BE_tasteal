using BE_tasteal.Business.Comment;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Persistence.Repository.AuthorRepo;
using BE_tasteal.Persistence.Repository.RecipeRepo;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("2.0")]
    public class CommentController : Controller
    {
        private readonly ICommentBusiness _commentBusiness;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserRepo _userRepo;
        public CommentController(
            ICommentBusiness commentBusiness,
            IRecipeRepository recipeRepository,
            IUserRepo userRepo)
        {
            _commentBusiness = commentBusiness;
            _recipeRepository = recipeRepository;
            _userRepo = userRepo;
        }

        [HttpGet]
        [Route("Recipe/{recipe_id}/Comments")]
        public async Task<IActionResult> getRecipeComments(int recipe_id)
        {

            try
            {
                if (await _recipeRepository.FindByIdAsync(recipe_id) == null)
                {
                    return BadRequest("Recipe id invalid");
                }
                var result = new
                {
                    recipe_id = recipe_id,
                    comments = await _commentBusiness.GetCommentByRecipeId(recipe_id)
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpPost]
        [Route("Recipe/{recipe_id}/Comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getRecipeComments(
             int recipe_id,
            [FromBody] CommentReq req)
        {

            try
            {
                if (await _recipeRepository.FindByIdAsync(recipe_id) == null)
                {
                    return BadRequest("Recipe id invalid");
                }
                if (await _userRepo.FindByIdAsync(req.account_id) == null)
                {
                    return BadRequest("User id invalid");
                }
                var result = await _commentBusiness.InsertCommentAsync(recipe_id, req);
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
        [Route("Recipe/{recipe_id}/Comments/{id}")]
        public async Task<IActionResult> putRecipeComment(
             int recipe_id, int id,
            [FromBody] CommentReqPut req)
        {

            try
            {
                if (await _recipeRepository.FindByIdAsync(recipe_id) == null)
                {
                    return BadRequest("Recipe id invalid");
                }
                if (await _commentBusiness.FindAsync(id) == null)
                {
                    return BadRequest("Comment id invalid");
                }
                var result = await _commentBusiness.UpdateCommentAsync(recipe_id, id, req);
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
        [Route("Recipe/{recipe_id}/Comments/{id}")]
        public async Task<IActionResult> deleteRecipeComment(
             int recipe_id, int id)
        {

            try
            {
                if (await _recipeRepository.FindByIdAsync(recipe_id) == null)
                {
                    return BadRequest("Recipe id invalid");
                }
                var comment = await _commentBusiness.FindAsync(id);
                if (comment == null)
                {
                    return BadRequest("Comment id invalid");
                }
                if (comment.recipe_id != recipe_id)
                {
                    return BadRequest("Comment does not belong to recipe");
                }

                var result = await _commentBusiness.DeleteCommentAsync(recipe_id, id);
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
