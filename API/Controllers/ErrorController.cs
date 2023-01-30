using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   public class ErrorController : BaseApiController
   {
      public ActionResult Error(int code)
      {
        return new ObjectResult(new ApiResponse(code));
      }
   }
}