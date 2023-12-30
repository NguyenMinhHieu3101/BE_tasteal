using BE_tasteal.API.AppSettings;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Repository.OccasionRepo;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class OccasionController : Controller
    {
        private readonly IOccasionRepo _occasionRepo;
        public OccasionController(
            IOccasionRepo occasionRepo)
        {
            _occasionRepo = occasionRepo;
        }
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> getRecipeComments()
        {

            try
            {
                var result = _occasionRepo.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpGet]
        [Route("{occasionId}")]
        public async Task<IActionResult> getRecipeComments(int occasionId)
        {

            try
            {
                var result = await _occasionRepo.FindByIdAsync(occasionId);
                if (result == null)
                {
                    return BadRequest("Occasion Id id invalid");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpPost]
        [Route("")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> postNewOccasion(OccasionReq occasionId)
        {
            try
            {
                var result = await _occasionRepo.InsertAsync(occasionId);
                if (result == null)
                {
                    return BadRequest("Insert fail");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpPut]
        [Route("")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> PutOccasion(OccasionPutReq item)
        {

            try
            {
                var result = await _occasionRepo.FindByIdAsync(item.id);
                if (result == null)
                {
                    return BadRequest("OccasionId invalid");
                }

                result.name = item.name;
                result.description = item.description;
                result.image = item.image;
                result.start_at = ConvertToDateTime(item.start_at);
                result.end_at = ConvertToDateTime(item.end_at);
                result.is_lunar_date = item.is_lunar_date;

                await _occasionRepo.UpdateAsync(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpDelete]
        [Route("{occasion_id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> postNewOccasion(int occasion_id)
        {

            try
            {
                var result = await _occasionRepo.FindByIdAsync(occasion_id);
                if (result == null)
                {
                    return BadRequest("Insert fail");
                }
                await _occasionRepo.DeleteAsync(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        private static DateTime ConvertToDateTime(string inputDateTime)
        {
            // Chuỗi ngày giờ đầu vào
            string input = "2023-12-29T08:45:30.123Z"; // Ví dụ chuỗi đầu vào

            // Định dạng của chuỗi đầu vào
            string format = "yyyy-MM-ddTHH:mm:ss.fffZ";

            // Chuyển đổi từ chuỗi ngày giờ sang kiểu DateTime
            if (DateTime.TryParseExact(input, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            else
            {
                throw new FormatException("Không thể chuyển đổi chuỗi ngày giờ.");
            }
        }

    }
}
