using BE_tasteal.Business.Ingredient;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.IngredientRepo;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class IngredientController : Controller
    {
        private readonly IIngredientBusiness _ingredientBusiness;
        private readonly IIngredientRepo _ingredientRepo;

        public IngredientController(
           IIngredientBusiness ingredientBusiness,
           IIngredientRepo ingredientRepo)
        {
            _ingredientBusiness = ingredientBusiness;
            _ingredientRepo = ingredientRepo;
        }

        [HttpPost]
        [Route("addfromexcel")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<IngredientEntity>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadExcelFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length <= 0)
                {
                    return BadRequest("Invalid file");
                }
                var ingredients = await _ingredientBusiness.AddFromExelAsync(file);
                return Created(string.Empty, ingredients);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex);
            }

        }

        [HttpPost]
        [Route("getall")]

        public async Task<IActionResult> GetAllIngredient(
            [FromBody] PageReq page)
        {
            try
            {
                var result = await _ingredientBusiness.GetAllIngredient(page);

                if (result.Item1.Count == 0)
                {
                    return NotFound();
                }

                var response = new
                {
                    maxPage = result.Item2,
                    ingredients = result.Item1
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.ToString()}");
                return BadRequest(ex);
            }
        }
        [HttpDelete]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(List<IngredientEntity>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> delete([FromQuery] int id)
        {
            try
            {
                var ingre = await _ingredientRepo.FindByIdAsync(id);
                if (ingre == null)
                {
                    return NotFound("id invalid");
                }

                await _ingredientRepo.DeleteAsync(ingre);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(false);
            }
        }
    }
}
