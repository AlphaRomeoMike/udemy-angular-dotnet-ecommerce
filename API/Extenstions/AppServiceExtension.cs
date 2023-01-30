using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace API.Extenstions
{
   public static class AppServiceExtension
   {
      public static IServiceCollection AddApplicationServices(this IServiceCollection services)
      {
         services.AddScoped<IProductRepository, ProductRepository>();
         services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
         services.Configure<ApiBehaviorOptions>(options =>
         {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
               var errors = actionContext.ModelState
                       .Where(e => e.Value.Errors.Count > 0)
                       .SelectMany(x => x.Value.Errors)
                       .Select(x => x.ErrorMessage).ToArray();

               var errorRes = new ApiValidationResponse
               {
                  Errors = errors
               };
               return new BadRequestObjectResult(errorRes);
            };
         });
         return services;
      }
   }
}