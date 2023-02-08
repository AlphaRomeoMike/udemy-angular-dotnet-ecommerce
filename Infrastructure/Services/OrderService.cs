using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;

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
      var basket = await  _basketRepo.GetBasketAsync(basketId);
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
      var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, Subtotal);
      _unitOfWork.Repository<Order>().Add(order);
      var result = await _unitOfWork.Complete();
      if (result <= 0) return null;
      await _basketRepo.DeleteBasketAsync(basketId);
      return order;
    }

    public Task<IList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
      throw new NotImplementedException();
    }

    public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
    {
      throw new NotImplementedException();
    }

    public Task<IList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
      throw new NotImplementedException();
    }
  }
}