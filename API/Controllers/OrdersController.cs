using API.DTOs;
using API.Errors;
using API.Extenstions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Authorize]
  public class OrdersController : BaseApiController
  {
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService orderService, IMapper mapper)
    {
      _orderService = orderService;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderToReturnDto>>> GetOrdersForUser()
    {
      var email = HttpContext.User.RetrieveEmailFromPrinciple();

      var orders = await _orderService.GetOrdersForUserAsync(email);
      return Ok(_mapper.Map<IList<OrderToReturnDto>>(orders));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
    {
      var email = HttpContext.User.RetrieveEmailFromPrinciple();

      var order = await _orderService.GetOrderByIdAsync(id, email);
      if (order == null) return NotFound(new ApiResponse(404));
      return Ok(_mapper.Map<OrderToReturnDto>(order));
    }

    [HttpGet("DeliveryMethods")]
    public async Task<ActionResult<List<DeliveryMethod>>> GetDeliveryMethods()
    {
      return Ok(await _orderService.GetDeliveryMethodsAsync());
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
    {
      var email = HttpContext.User.RetrieveEmailFromPrinciple();
      var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);
      var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);
      if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));
      return Ok(order);
    }
  }
}