using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ValuesController : BaseApiController
    {
        private readonly IPaymentService _paymentService;

        public ValuesController(
            IPaymentService paymentService
            )
        {
            _paymentService = paymentService;
        }

        [Authorize]
        [HttpPost("basketId")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            return await _paymentService.CreateOrUpdatePaymentIntent(basketId);
        }
    }
}
