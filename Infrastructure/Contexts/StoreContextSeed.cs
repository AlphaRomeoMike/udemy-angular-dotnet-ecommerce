using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Contexts
{
    #nullable disable
   public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory logger)
        {
            try {
                if (!context.ProductBrand.Any())
                {
                    var brandsData = File.ReadAllText("./Infrastructure/Data/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach (var item in brands!)
                    {
                        context.ProductBrand.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.ProductType.Any())
                {
                    var typesData = File.ReadAllText("./Infrastructure/Data/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    foreach (var item in types!)
                    {
                        context.ProductType.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("./Infrastructure/Data/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
                
            } 
            catch (Exception ex)
            {
                var log = logger.CreateLogger<StoreContextSeed>();
                log.LogError(ex.Message);
            }
        }
    }
}