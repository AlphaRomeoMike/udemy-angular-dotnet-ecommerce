using System.Text;
using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Extenstions
{
   public static class IdentityServiceExtension
   {
      public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
      {
         services.AddDbContext<AppIdentityDbContext>(options =>
         {
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
         });

         services.AddIdentityCore<AppUser>(options =>
         {
         })
         .AddEntityFrameworkStores<AppIdentityDbContext>()
         .AddSignInManager<SignInManager<AppUser>>();
         
         services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options => 
         {
            options.TokenValidationParameters = new TokenValidationParameters
            {
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
               ValidIssuer = configuration["Token:Issuer"],
               ValidateIssuer = true,
               ValidateAudience = false
            };
         });

         services.AddAuthorization();
         return services;
      }
   }
}