namespace SimpleMarket.Carrier.Domain.OrderAggregate;


public class Order
{
    public Guid Id { get; set; }
    public string ShippingAddress { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    
    public decimal ShippingAmount { get; set; }
    public OrderState State { get; set; }
    
    public ICollection<OrderItem>? Items { get; set; }
}