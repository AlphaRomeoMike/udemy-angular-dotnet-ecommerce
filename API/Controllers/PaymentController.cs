using API.Errors;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace API.Controllers
{
  public class PaymentController : BaseApiController
  {
    private const string WhSecret = "whsec_4b159bbd459aa7b6230cf4a5f673e19ec5c0fe234b51985a6c26b3723dae0448";
    private readonly IPaymentService _paymentService;
    private readonly ILogger<PaymentController> _logger;


    public PaymentController(
      IPaymentService paymentService,
      ILogger<PaymentController> logger)
    {
      _paymentService = paymentService;
      _logger = logger;
    }

    [Authorize]
    [HttpPost("{basketId}")]
    public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
    {
      var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
      if (basket == null) return BadRequest(new ApiResponse(400, "Problem with your basket"));
      return basket;
    }

    [HttpPost("webhook")]
    public async Task<ActionResult> StripeWebHook()
    {
      var json = await new StreamReader(Request.Body).ReadToEndAsync();
      var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WhSecret);
      PaymentIntent intent;
      Order order;

      switch (stripeEvent.Type)
      {
        case "payment_intent.succeeded":
          intent = (PaymentIntent)stripeEvent.Data.Object;
          _logger.LogInformation("Payment succeeded: ", intent.Id);
          order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
          _logger.LogDebug("Order updated to payment receievd ", order.Id);
          break;
        case "payment_intent.payment_failed":
          intent = (PaymentIntent)stripeEvent.Data.Object;
          _logger.LogInformation("Payment failed: ", intent.Id);
          order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
          _logger.LogDebug("Order updated to payment failed ", order.Id);

          break;
      }

      return new EmptyResult();
    }
  }
}
