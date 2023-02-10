using API.Errors;
using API.Helpers;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Contexts;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API.Extenstions
{
   public static class AppServiceExtension
   {
      public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
      {
         services.AddDbContext<StoreContext>(x =>
         {
            x.UseSqlServer(config.GetConnectionString("DefaultConnection"));
         });
         services.AddSingleton<IConnectionMultiplexer>(c =>
         {
            var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
            return ConnectionMultiplexer.Connect(options);
         });
         services.AddAutoMapper(typeof(MappingProfiles));
         services.AddControllers();
         services.AddScoped<IProductRepository, ProductRepository>();
         services.AddScoped<IUnitOfWork, UnitOfWork>();
         services.AddScoped<IBasketRepository, BasketRepository>();
         services.AddScoped<IPaymentService, PaymentService>();
         services.AddScoped<ITokenService, TokenService>();
         services.AddScoped<IOrderService, OrderService>();
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