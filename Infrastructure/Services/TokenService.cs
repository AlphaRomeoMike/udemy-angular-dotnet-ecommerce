using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
   public class TokenService : ITokenService
   {
      private readonly IConfiguration _configuration;
      private readonly SymmetricSecurityKey _key;
      public TokenService(IConfiguration configuration)
      {
         _configuration = configuration;
         _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
      }

      public string CreateToken(AppUser user)
      {
         var claims = new List<Claim>
         {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.DisplayName),
         };

         var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
         var tokenDescription = new SecurityTokenDescriptor
         {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(1),
            SigningCredentials = creds,
            Issuer = _configuration["Token:Issuer"]
         };

         var tokenHandler = new JwtSecurityTokenHandler();
         var token = tokenHandler.CreateToken(tokenDescription);
         return tokenHandler.WriteToken(token);
      }
   }
}