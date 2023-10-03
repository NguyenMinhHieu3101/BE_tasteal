using BE_tasteal.API.AppSettings;
using BE_tasteal.Business.Interface;
using BE_tasteal.Entity.DTO;
using BE_tasteal.Entity.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BE_tasteal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiVersion("1.0")]
    public class SanPhamController : Controller
    {
        // private readonly ISanPhamResposity _sanPhamResposity;
        private readonly IBusiness<SanPhamDto, SanPhamEntity> _sanPhambusiness;

        public SanPhamController(
            IBusiness<SanPhamDto, SanPhamEntity> sanPhambusiness)
        {
            // _sanPhamResposity = sanPhamResposity;
            _sanPhambusiness = sanPhambusiness;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SanPhamEntity))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var sanpham = await _sanPhambusiness.GetAll();
                return Ok(sanpham);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return NotFound();
            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SanPhamEntity))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddDayAsync([FromBody] SanPhamDto sanPham)
        {
            return Created(string.Empty, await _sanPhambusiness.Add(sanPham));
        }
    }
}
