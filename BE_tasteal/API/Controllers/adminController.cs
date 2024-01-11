using BE_tasteal.API.AppSettings;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.CommentRepo;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("2.0")]
    public class adminController : Controller
    {
        private readonly ICommentRepo commentRepo;
        public adminController(ICommentRepo comment)
        {
            commentRepo = comment;
        }
        [HttpPost]
        [Route("Comment/getAll")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public ActionResult GetAllComment(PageReq req)
        {
            try
            {
                return Ok(commentRepo.getAll(req));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpGet]
        [Route("Comment/{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> GetComment(int id)
        {
            try
            {
                var cmt = await commentRepo.FindByIdAsync(id);
                if (cmt == null)
                    return BadRequest("Cmt id not found");
                return Ok(cmt);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpPut]
        [Route("Comment")]
        public async Task<ActionResult> PutComment(CommentEntity req)
        {
            try
            {
                var cmt = await commentRepo.FindByIdAsync(req.id);
                if (cmt == null)
                    return BadRequest("Cmt id not found");

                cmt.recipe_id = req.recipe_id;
                cmt.account_id = req.account_id;
                cmt.comment = req.comment;
                cmt.image = req.image;
                cmt.updated_at = DateTime.Now;

                await commentRepo.UpdateAsync(cmt);

                return Ok(cmt);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpDelete]
        [Route("Comment/softDelete/{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> deleteComment(int id)
        {
            try
            {
                var cmt = await commentRepo.FindByIdAsync(id);
                if (cmt == null)
                    return BadRequest("Cmt id not found");

                await commentRepo.deleteSoft(id);

                return Ok(cmt);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpDelete]
        [Route("Comment/hardDelete/{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> deleteCommentHard(int id)
        {
            try
            {
                var cmt = await commentRepo.FindByIdAsync(id);
                if (cmt == null)
                    return BadRequest("Cmt id not found");

                await commentRepo.DeleteAsync(cmt);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
