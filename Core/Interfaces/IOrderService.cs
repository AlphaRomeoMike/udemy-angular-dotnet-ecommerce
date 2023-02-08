using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
  public interface IOrderService
  {
    Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress);
    Task<IList<Order>> GetOrdersForUserAsync(string buyerEmail);
    Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
    Task<IList<DeliveryMethod>> GetDeliveryMethodsAsync();
  }
}