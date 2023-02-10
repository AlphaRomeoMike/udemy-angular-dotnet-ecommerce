namespace Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
        }

        public Order(IList<OrderItem> orderItems, string buyerEmail, Address shippedTo, DeliveryMethod deliveryMethod, decimal subTotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippedToAddress = shippedTo;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public Address ShippedToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Price;
        }
    }
}
