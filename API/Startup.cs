
using Core.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AutoMapper;
using API.Helpers;
using API.Middlewares;
using Microsoft.AspNetCore.Mvc;
using API.Errors;
using API.Extenstions;

namespace API
{
   public class Startup
   {
      private readonly IConfiguration _configuration;
      public Startup(IConfiguration configuration)
      {
         this._configuration = configuration;
      }
      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddApplicationServices(_configuration);
         services.AddIdentityServices(_configuration);
         services.AddSwaggerDocumentation();
         services.AddCors(opt =>
         {
            opt.AddPolicy("CorsPolicy", policy =>
            {
               policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200")
                  .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE");
            });
         });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         app.UseMiddleware<ExceptionMiddleware>();
         if (env.IsDevelopment())
         {
            app.UseSwaggerDocumentation();
         }
         app.UseStatusCodePagesWithReExecute("/err/{0}");

         app.UseHttpsRedirection();

         app.UseRouting();
         app.UseStaticFiles();
         app.UseCors("CorsPolicy");
         app.UseAuthentication();
         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
