using Core.Entities.OrderAggregate;

namespace API.DTOs
{
  public class OrderToReturnDto
  {
    public int Id { get; set; }
    public string BuyerEmail { get; set; }
    public DateTime OrderDate { get; set; }
    public Address ShippedToAddress { get; set; }
    public decimal ShippingPrice { get; set; }
    public string DeliveryMethod { get; set; }
    public IList<OrderItemDto> OrderItems { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
  }
}