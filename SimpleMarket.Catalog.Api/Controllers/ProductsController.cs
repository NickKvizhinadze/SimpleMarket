using DotNetHelpers.Models;
using Microsoft.AspNetCore.Mvc;
using SimpleMarket.Catalog.Api.Models;
using SimpleMarket.Catalog.Api.Services;

namespace SimpleMarket.Catalog.Api.Controllers;

[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _service;

    public ProductsController(IProductsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Paging paging, CancellationToken cancellationToken)
    {
        var customers = await _service.GetProducts(paging, cancellationToken);

        return Ok(customers);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.GetProduct(id, cancellationToken);

        if (!result.Succeeded)
            return NotFound();

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCreateDto model, CancellationToken cancellationToken)
    {
        var result = await _service.CreateProduct(model, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return CreatedAtAction(nameof(Get), result.Data);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductUpdateDto model,
        CancellationToken cancellationToken)
    {
        var result = await _service.UpdateProduct(id, model, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(result.Data);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.DeleteProduct(id, cancellationToken);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return NoContent();
    }
}