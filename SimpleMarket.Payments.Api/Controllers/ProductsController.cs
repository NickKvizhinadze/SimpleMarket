using Microsoft.AspNetCore.Mvc;
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePaymentDto model, CancellationToken cancellationToken)
    {
        var result = await _service.CreatePayment(model, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok();
    }
}