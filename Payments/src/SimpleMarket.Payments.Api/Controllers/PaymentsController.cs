using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;
using SimpleMarket.Payments.Api.Models;
using SimpleMarket.Payments.Api.Services;

namespace SimpleMarket.Payments.Api.Controllers;

[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _service;

    public PaymentsController(IPaymentService service)
    {
        _service = service;
    }

    
    //TODO: remove this
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePaymentDto model, CancellationToken cancellationToken)
    {
        var orderId = Baggage.Current.GetBaggage("order.id");
        Activity.Current?.AddTag("order.id", orderId);
        
        var result = await _service.CreatePayment(model, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok();
    }
}