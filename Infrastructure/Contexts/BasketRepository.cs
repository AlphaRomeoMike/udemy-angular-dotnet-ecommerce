using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Contexts
{
   public class BasketRepository : IBasketRepository
   {
      public readonly IDatabase _database;
      public BasketRepository(IConnectionMultiplexer redis)
      {
         _database = redis.GetDatabase();
      }

      public async Task<bool> DeleteBasketAsync(string baskedId)
      {
         return await _database.KeyDeleteAsync(baskedId);
      }

      public async Task<CustomerBasket> GetBasketAsync(string baskedId)
      {
        var data = await _database.StringGetAsync(baskedId);
        return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
      }

      public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
      {
         var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

         if (!created) return null;

         return await GetBasketAsync(basket.Id);
      }
   }
}