using System.Text.Json;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Contexts
{
    #nullable disable
   public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory logger)
        {
            try {
                if (!context.Shops.Any())
                {
                    var shopData = File.ReadAllText("../Infrastructure/Context/Data/shops.json");
                    var data = JsonSerializer.Deserialize<List<Shop>>(shopData);
                    foreach (var shop in data)
                    {
                        context.Shops.Add(shop);
                    }

                }
                if (!context.Vendors.Any())
                {
                    var vendorData = File.ReadAllText("../Infrastructure/Context/Data/vendors.json");
                    var data = JsonSerializer.Deserialize<List<Vendor>>(vendorData);
                    foreach (var shop in data)
                    {
                        context.Vendors.Add(shop);
                    }
                }
                if (!context.ProductBrand.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Contexts/Data/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach (var item in brands!)
                    {
                        context.ProductBrand.Add(item);
                    }
                }
                if (!context.ProductType.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/Contexts/Data/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    foreach (var item in types!)
                    {
                        context.ProductType.Add(item);
                    }
                }
                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Contexts/Data/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }
                }
                if (!context.DeliveryMethods.Any())
                {
                    var deliveryData = File.ReadAllText("../Infrastructure/Contexts/Data/delivery.json");
                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                    foreach (var item in methods)
                    {
                        context.DeliveryMethods.AddRange(item);
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