using SimpleMarket.Carrier.Domain.OrderAggregate;

namespace SimpleMarket.Carrier.Application.Orders.Models;

public class CreateOrderDto
{
    public CreateOrderDto(Guid correlationId, Guid customerId, string shippingAddress, decimal shippingAmount, List<OrderItemDto> items)
    {
        CorrelationId = correlationId;
        CustomerId = customerId;
        ShippingAddress = shippingAddress;
        ShippingAmount = shippingAmount;
        Items = items;
    }

    public Guid CorrelationId { get; set; }
    public Guid CustomerId { get; set; }
    public string ShippingAddress { get; set; }
    public decimal ShippingAmount { get; set; }
    public List<OrderItemDto> Items { get; set; }
}

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}