using BE_tasteal.Business.Nutrition;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class NutritionController : Controller
    {
        private readonly INutritionBusiness<Nutrition_InfoDto, Nutrition_InfoEntity> _nutritionBusiness;

        public NutritionController(
           INutritionBusiness<Nutrition_InfoDto, Nutrition_InfoEntity> nutritionBusiness)
        {
            _nutritionBusiness = nutritionBusiness;
        }


    }
}
