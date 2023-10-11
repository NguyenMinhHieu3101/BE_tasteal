using BE_tasteal.Business.Ingredient;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class NutritionController : Controller
    {
        private readonly IIngredientBusiness _ingredientBusiness;

        public NutritionController(
           IIngredientBusiness ingredientBusiness)
        {
            _ingredientBusiness = ingredientBusiness;
        }

        [HttpPost]
        [Route("addfromexcel")]
        public IActionResult UploadExcelFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length <= 0)
                {
                    return BadRequest("Invalid file");
                }
                var ingredient = _ingredientBusiness.AddFromExel(file);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex);
            }

        }
    }
}
