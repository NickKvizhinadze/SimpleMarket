﻿namespace SimpleMarket.Orders.Contracts;

public class OrderCreated
{
    public int Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public Decimal TotalAmount { get; set; }
}