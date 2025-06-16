using SimpleMarket.Orders.Domain.Entities;

namespace SimpleMarket.Orders.Application.Infrastructure.Services.Carrier;

public interface ICarrierClient
{
    Task CreateOrder(Order order);
}