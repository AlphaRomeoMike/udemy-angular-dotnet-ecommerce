using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
   public class AppIdentityDbContextSeed
   {
      public static async Task SeedUserAsync(UserManager<AppUser> userManager)
      {
         if (!userManager.Users.Any())
         {
            var user = new AppUser
            {
                Email = "bob@example.com",
                DisplayName = "Bob Wilson",
                UserName = "bob@example.com",
                Address = new Address
                {
                   FirstName = "Bob",
                   LastName = "Wilson Parker",
                   Street = "10th Street",
                   City = "New York",
                   State = "NY",
                   ZipCode = "90210"
                }
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
      }
   }
}
}