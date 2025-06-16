using SimpleMarket.Carrier.Grpc.Orders;
using SimpleMarket.Orders.Domain.Entities;

namespace SimpleMarket.Orders.Application.Infrastructure.Services.Carrier;

public class CarrierClient: ICarrierClient
{
    private readonly OrderServiceDefinition.OrderServiceDefinitionClient _grpcClient;

    public CarrierClient(OrderServiceDefinition.OrderServiceDefinitionClient grpcClient)
    {
        _grpcClient = grpcClient;
    }

    public async Task CreateOrder(Order order)
    {
        var response = await _grpcClient.CreateAsync(new CreateOrderRequest
        {
            CustomerId = order.CustomerId.ToString(),
            OrderId = order.Id.ToString(),
            ShippingAddress = order.ShippingAddressId.ToString(),
            PaymentMethod = (CreateOrderRequest.Types.PaymentMethodType)order.PaymentMethod,
            Amount = (double)order.Amount,
            ShippingAmount = (double)order.ShippingAmount
        });
        
        if(!response.IsSuccess)
            throw new Exception("Order not created in carrier service");
    }

}