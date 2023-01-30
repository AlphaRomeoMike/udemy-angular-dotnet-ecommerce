using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
   public class ApiResponse
   {
      public ApiResponse(int statusCode, string message = null)
      {
         StatusCode = statusCode;
         Message = message ?? GetDefaultMessageForStatusCode(statusCode);
      }
      public int StatusCode { get; set; }
      public string Message { get; set; }
      private string GetDefaultMessageForStatusCode(int statusCode)
      {
         return statusCode switch {
            400 => "Bad Request!",
            401 => "Unauthorized",
            404 => "Resource Not Found!",
            500 => "Server Error!",
            _ => null
         };
      }

   }
}