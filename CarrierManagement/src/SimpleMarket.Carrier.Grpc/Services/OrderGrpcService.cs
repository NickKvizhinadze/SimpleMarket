using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SimpleMarket.Carrier.Application.Orders.Models;
using SimpleMarket.Carrier.Grpc.Orders;
using SimpleMarket.Carrier.Application.Orders.Services;

namespace SimpleMarket.Carrier.Grpc.Services;

public class OrderGrpcService : OrderServiceDefinition.OrderServiceDefinitionBase
{
    private readonly IOrderService _service;

    public OrderGrpcService(IOrderService service)
    {
        _service = service;
    }

    public override async Task<Result> Create(CreateOrderRequest request, ServerCallContext context)
    {
        var dto = new CreateOrderDto(Guid.Parse(request.OrderId),
            Guid.Parse(request.CustomerId),
            request.ShippingAddress,
            (decimal)request.ShippingAmount,
            request.OrderItems.Select(i => new OrderItemDto
            {
                ProductId = Guid.Parse(i.ProductId),
                Quantity = i.Quantity,
            }).ToList()
        );

        var result = await _service.Create(dto, context.CancellationToken);
        
        return new Result
        {
            IsSuccess = result.Succeeded,
            Data = Any.Pack(new CreateOrderRequest.Types.CreateOrderResponse
            {
                OrderId = request.OrderId
            })
        };
    }
}
