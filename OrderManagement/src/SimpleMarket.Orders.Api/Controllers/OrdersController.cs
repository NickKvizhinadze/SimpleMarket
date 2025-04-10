using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;
using SimpleMarket.Orders.Api.Models;
using SimpleMarket.Orders.Api.Services;
using SimpleMarket.Orders.Api.Diagnostics.Extensions;

namespace SimpleMarket.Orders.Api.Controllers;

[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _service;
    private readonly IPaymentService _paymentService;

    public OrdersController(IOrdersService service, IPaymentService paymentService)
    {
        _service = service;
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RequestCheckoutDto model, CancellationToken cancellationToken)
    {
        Activity.Current.EnrichWithOrderRequestData(model);
        var result = await _service.Checkout(model, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        var orderData = result.Data;
        Activity.Current.EnrichWithOrderData(orderData!);
        Baggage.SetBaggage("order.id", orderData!.Id.ToString());
        
        await _paymentService.PayOrder(new PayOrderDto
            {
                CorrelationId = orderData!.Id,
                CustomerId = model.CustomerId,
                PaymentMethod = model.PaymentMethod,
                Amount = orderData.Amount
            },
            cancellationToken);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var orders = await _service.GetOrders(cancellationToken);

        return Ok(orders);
    }
}