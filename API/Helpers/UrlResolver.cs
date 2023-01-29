using Core.Entities;
using AutoMapper;
using API.DTOs;

namespace API.Helpers
{
   // For products only
   public class UrlResolver : IValueResolver<Product, ProductToReturnDto, string>
   {
      private readonly IConfiguration _config;

      public UrlResolver(Microsoft.Extensions.Configuration.IConfiguration config)
      {
         _config = config;
      }

      public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
      {
         if (!string.IsNullOrEmpty(source.PictureUrl))
         {
            return _config["ApiUrl"] + source.PictureUrl;
         }
         return null;
      }
   }
}