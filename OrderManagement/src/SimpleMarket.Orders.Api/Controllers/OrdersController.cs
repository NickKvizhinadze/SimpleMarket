using Microsoft.AspNetCore.Mvc;
using SimpleMarket.Orders.Application.Orders.Models;
using SimpleMarket.Orders.Application.Orders.Services;

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
        var result = await _service.Checkout(model, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        var orderData = result.Data;
        
        return Ok(orderData!.Id);
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var orders = await _service.GetOrders(cancellationToken);

        return Ok(orders);
    }
}