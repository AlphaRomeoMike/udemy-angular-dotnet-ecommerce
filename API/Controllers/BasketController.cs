using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   public class BasketController : BaseApiController
   {
      private readonly ILogger<BasketController> _logger;
      private readonly IBasketRepository _basketRepository;

      public BasketController(ILogger<BasketController> logger, IBasketRepository basketRepository)
      {
         _logger = logger;
         _basketRepository = basketRepository;
      }
      [HttpGet]
      public async Task<ActionResult<CustomerBasket>> GetBasketsById(string id)
      {
         var basket = await _basketRepository.GetBasketAsync(id);

         return Ok(basket ?? new CustomerBasket(id));
      }

      [HttpPost]
      public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
      {
         var newBasket = await _basketRepository.UpdateBasketAsync(basket);

         return newBasket;
      }

      [HttpDelete]
      public async Task DeleteBasket(string basket)
      {
          await _basketRepository.DeleteBasketAsync(basket);
      }
   }
}