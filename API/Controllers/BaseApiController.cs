using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class BaseApiController : Controller
   {

      public BaseApiController()
      {
      }
   }
}