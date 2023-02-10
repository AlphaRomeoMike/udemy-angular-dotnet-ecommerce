using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services
{
  public class OrderService : IOrderService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBasketRepository _basketRepo;

    public OrderService(
      IUnitOfWork unitOfWork,
      IBasketRepository basketRepo)
    {
      _unitOfWork = unitOfWork;
      _basketRepo = basketRepo;
    }

    public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
    {
      var basket = await _basketRepo.GetBasketAsync(basketId);
      var items = new List<OrderItem>();
      foreach (var item in basket.Items)
      {
        var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
        var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
        var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
        items.Add(orderItem);
      }
      var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
      var Subtotal = items.Sum(item => item.Price * item.Quantity);
      var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);
      var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
      if (order != null)
      {
        order.ShippedToAddress = shippingAddress;
        order.DeliveryMethod = deliveryMethod;
        order.SubTotal = Subtotal;
        _unitOfWork.Repository<Order>().Update(order);
      }
      else
      {
        order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, Subtotal, basket.PaymentIntentId);
        _unitOfWork.Repository<Order>().Add(order);
      }
      var result = await _unitOfWork.Complete();
      if (result <= 0) return null;
      // TODO: Handle this later
      // await _basketRepo.DeleteBasketAsync(basketId); 
      return order;
    }

    public async Task<IList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
      return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
    {
      var spec = new OrderWithItemsAndOrderingSpecification(id, buyerEmail);
      return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
    }

    public async Task<IList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
      var spec = new OrderWithItemsAndOrderingSpecification(buyerEmail);
      return await _unitOfWork.Repository<Order>().ListAsync(spec);
    }
  }
}