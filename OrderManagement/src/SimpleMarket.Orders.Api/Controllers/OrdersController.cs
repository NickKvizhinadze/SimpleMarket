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

    public OrdersController(IOrdersService service)
    {
        _service = service;
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
        
        //TODO: check if baggage goes in Rabbit
        Baggage.SetBaggage("order.id", orderData!.Id.ToString());
        
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var orders = await _service.GetOrders(cancellationToken);

        return Ok(orders);
    }
}