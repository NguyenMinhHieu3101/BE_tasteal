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
    public class RecipeSearchController : Controller
    {
    }
}
