using DotNetHelpers.Models;
using Microsoft.AspNetCore.Mvc;
using SimpleMarket.Customers.Api.Models;
using SimpleMarket.Customers.Api.Services;

namespace SimpleMarket.Customers.Api.Controllers;

[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomersController(ICustomerService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Paging paging, CancellationToken cancellationToken)
    {
        var customers = await _service.GetCustomers(paging, cancellationToken);

        return Ok(customers);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.GetCustomer(id, cancellationToken);

        if (!result.Succeeded)
            return NotFound();

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CustomerCreateDto model, CancellationToken cancellationToken)
    {
        var result = await _service.CreateCustomer(model, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return CreatedAtAction(nameof(Get), result.Data);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CustomerUpdateDto model,
        CancellationToken cancellationToken)
    {
        var result = await _service.UpdateCustomer(id, model, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(result.Data);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.DeleteCustomer(id, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return NoContent();
    }
}